using MusicCenterModels;
using System.Data;

namespace MusicCenterWebService.Repositories
{
    public class RequestRepository : Repository, IRepository<Request>
    {
        public bool Create(Request entity)
        {
            string sql = @"INSERT INTO Requests(Title, Description, isSeen, isApproved, SenderID, RecieverID, RequestType)
                         values(@Title, @Description, @isSeen, @isApproved, @SenderID, @RecieverID, @RequestType)";
            GetDbContext()
                .AddParameter("@Title", entity.Title)
                .AddParameter("@Description", entity.Description)
                .AddParameter("@isSeen", ((bool)entity.IsSeen ? "1":"0"))
                .AddParameter("@isApproved", (bool)entity.IsApproved ? "1":"0")
                .AddParameter("@SenderID", entity.Sender.Id)
                .AddParameter("@RecieverID", entity.Reciever.Id)
                .AddParameter("@RequestType", entity.RequestType);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public bool Delete(Request entity)
        {
            string sql = "DELETE FROM Requests WHERE RequestID=@RequestId";
            GetDbContext().AddParameter("@RequestId", entity.Id);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public List<Request> GetAll()
        {
            List<Request> list = new List<Request>();
            string sql = "SELECT * FROM Requests";
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().RequestCreator.CreateModel(reader));
                }
            }
            return list;
        }
        public List<Request> GetSentByUserID(string userID)
        {
            List<Request> list = new List<Request>();
            string sql = "SELECT * FROM Requests WHERE SenderID=@SenderId";
            GetDbContext().AddParameter("@SenderId", userID);
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().RequestCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return list;
        }
        public List<Request> GetRecievedByUserID(string userID)
        {
            List<Request> list = new List<Request>();
            string sql = "SELECT * FROM Requests WHERE RecieverID=@RecieverID";
            GetDbContext().AddParameter("@RecieverID", userID);
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().RequestCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return list;
        }

        public Request GetById(string id)
        {
            string sql = "SELECT * FROM Requests WHERE RequestID=@RequestId";
            GetDbContext().AddParameter("@RequestId", id);
            IDataReader reader = GetDbContext().ReadValue(sql);
            Request res = null;
            while (reader.Read())
            {
                res = GetModelFactory().RequestCreator.CreateModel(reader);
            }
            reader.Dispose();
            GetDbContext().ClearParameters();
            return res;
        }

        public bool Update(Request entity)
        {
            string sql = @"UPDATE Requests 
                        SET Title=@Title, Description=@Description, isSeen=@isSeen, 
                            isApproved=@isApproved, RequestType=@RequestType
                        WHERE RequestID=@RequestId";
            GetDbContext()
                .AddParameter("@Title", entity.Title)
                .AddParameter("@Description", entity.Description)
                .AddParameter("@isSeen", (bool)entity.IsSeen ? "1" : "0")
                .AddParameter("@isApproved", (bool)entity.IsApproved ? "1" : "0")
                .AddParameter("@RequestType", entity.RequestType)
                .AddParameter("@RequestId", entity.Id);
            int res = GetDbContext().Update(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
    }
}
