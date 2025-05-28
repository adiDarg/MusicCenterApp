using MusicCenterModels;
using System.Data;

namespace MusicCenterWebService.Repositories
{
    public class MeetingRepository : Repository, IRepository<Meeting>
    {
        public bool Create(Meeting entity)
        {
            string sql = @"INSERT INTO Meetings(GroupID,MeetingDate,Room)
                         values(@GroupID,@MeetingDate,@Room)";
            GetDbContext()
                .AddParameter("@GroupID", entity.Group.Id)
                .AddParameter("@MeetingDate", entity.Date)
                .AddParameter("@Room", entity.Room);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public bool Delete(Meeting entity)
        {
            string sql = "DELETE FROM Meetings WHERE MeetingID=@MeetingId";
            GetDbContext().AddParameter("@MeetingId", entity.Id);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public List<Meeting> GetAll()
        {
            List<Meeting> list = new List<Meeting>();
            string sql = "SELECT * FROM Meetings";
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().MeetingCreator.CreateModel(reader));
                }
            }
            return list;
        }
        public List<Meeting> GetByGroupID(string groupID)
        {
            List<Meeting> list = new List<Meeting>();
            string sql = "SELECT * FROM Meetings WHERE GroupID=@GroupId";
            GetDbContext().AddParameter("@GroupId", groupID);
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().MeetingCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return list;
        }
        public Meeting GetById(string id)
        {
            string sql = "SELECT * FROM Meetings WHERE MeetingID=@MeetingId";
            GetDbContext().AddParameter("@MeetingId", id);
            IDataReader reader = GetDbContext().ReadValue(sql);
            Meeting res = null;
            while (reader.Read())
            {
                res = GetModelFactory().MeetingCreator.CreateModel(reader);
            }
            reader.Dispose();
            GetDbContext().ClearParameters();
            return res;
        }

        public bool Update(Meeting entity)
        {
            string sql = @"UPDATE Meetings 
                        SET GroupID=@GroupID,MeetingDate=@MeetingDate,Room=@Room
                        WHERE MeetingID=@MeetingId";
            GetDbContext()
                .AddParameter("@MeetingDate", entity.Date)
                .AddParameter("@GroupID", entity.Group.Id)
                .AddParameter("@Room", entity.Room)
                .AddParameter("@MeetingId", entity.Id);
            int res = GetDbContext().Update(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
    }
}
