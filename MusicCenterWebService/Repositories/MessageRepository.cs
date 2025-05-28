using MusicCenterModels;
using System.Collections.Generic;
using System.Data;

namespace MusicCenterWebService.Repositories
{
    public class MessageRepository : Repository, IRepository<Message>
    {
        public bool Create(Message entity)
        {
            string sql = @"INSERT INTO Messages(Title,Description)
                         values(@Title,@Description)";
            GetDbContext()
                .AddParameter("@Title", entity.Title)
                .AddParameter("@Description", entity.Description);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public bool Delete(Message entity)
        {
            string sql = "DELETE FROM Messages WHERE MessageID=@MessageId";
            GetDbContext().AddParameter("@MessageId", entity.Id);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public List<Message> GetAll()
        {
            List<Message> list = new List<Message>();
            string sql = "SELECT * FROM Messages";
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().MessageCreator.CreateModel(reader));
                }
            }
            return list;
        }
        public List<Message> GetByUserID(string userID)
        {
            List<Message> list = new List<Message>();
            string sql = @"SELECT Messages.MessageID, Messages.Title, Messages.Description, UsersMessages.RecieverID
                        FROM Messages INNER JOIN UsersMessages ON Messages.MessageID = UsersMessages.MessageID WHERE (((UsersMessages.RecieverID)=@RecieverId));";
            GetDbContext().AddParameter("@RecieverId", userID);
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().MessageCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return list;
        }

        public Message GetById(string id)
        {
            string sql = "SELECT * FROM Messages WHERE MessageID=@MessageId";
            GetDbContext().AddParameter("@MessageId", id);
            IDataReader reader = GetDbContext().ReadValue(sql);
            Message res = null;
            while (reader.Read())
            {
                res = GetModelFactory().MessageCreator.CreateModel(reader);
            }
            reader.Dispose();
            GetDbContext().ClearParameters();
            return res;
        }

        public bool Update(Message entity)
        {
            string sql = @"UPDATE Messages SET Title=@Title,Description=@Description 
                            WHERE MessageID=@MessageId";
            GetDbContext()
                .AddParameter("@Title", entity.Title)
                .AddParameter("@Description", entity.Description)
                .AddParameter("@MessageId", entity.Id);
            int res = GetDbContext().Update(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
        public bool AddReciever(string messageID, string userID) {
            string sql = @"INSERT INTO UsersMessages(RecieverID,MessageID) values(@userID,@messageID)";
            GetDbContext()
                .AddParameter("@userID", userID)
                .AddParameter("@messageID", messageID);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
        public Message? GetByTitleAndDescription(string title, string description)
        {
            string sql = @"SELECT * FROM Messages WHERE Title=@Title AND Description=@Description";
            GetDbContext()
                .AddParameter("@Title", title)
                .AddParameter("@Description", description);
            Message res = null;
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    res = (GetModelFactory().MessageCreator.CreateModel(reader));
                    break;
                }
            }
            GetDbContext().ClearParameters();
            return res;
        }
    }
}
