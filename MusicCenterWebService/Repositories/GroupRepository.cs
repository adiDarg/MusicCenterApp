using MusicCenterModels;
using System.Data;
using Group = MusicCenterModels.Group;

namespace MusicCenterWebService.Repositories
{
    public class GroupRepository : Repository, IRepository<Group>
    {
        public bool Create(Group entity)
        {
            string sql = @"INSERT INTO Groups(GroupName,InstructorID,Room)
                         values(@GroupName, @InstructorID, @Room)";
            GetDbContext()
                .AddParameter("@GroupName", entity.Name)
                .AddParameter("@InstructorID", entity.Instructor.Id)
                .AddParameter("@Room", entity.Room);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
        public bool AddRegistreeToGroup(string groupID, string registreeID)
        {
            string sql = @"INSERT INTO RegistreesGroups(RegistreeID,GroupID)
                         values(@RegistreeID,@GroupID)";
            GetDbContext()
                .AddParameter("@RegistreeID", registreeID)
                .AddParameter("GroupID", groupID);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public bool Delete(Group entity)
        {
            string sql = @"DELETE FROM Groups WHERE GroupID=@GroupID";
            GetDbContext().AddParameter("@GroupID", entity.Id);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public bool RemoveUser(string groupID, string userID)
        {
            string sql = @"DELETE FROM RegistreesGroups 
                           WHERE RegistreeID=@RegistreeID AND GroupID=@GroupID";
            GetDbContext().AddParameter("@RegistreeID", userID)
                .AddParameter("@GroupID", groupID);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public List<Group> GetAll()
        {
            List<Group> list = new List<Group>();
            string sql = "SELECT * FROM Groups";
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().GroupCreator.CreateModel(reader));
                }
            }
            return list;
        }
        public List<Group> GetByUserID(string userId)
        {
            List<Group> list = new List<Group>();
            string sql = @"SELECT Groups.GroupID, Groups.GroupName, Groups.InstructorID, Groups.Room, RegistreesGroups.RegistreeID
                        FROM Groups INNER JOIN RegistreesGroups ON Groups.GroupID = RegistreesGroups.GroupID
                        WHERE (((RegistreesGroups.RegistreeID)=@RegistreeID));";
            GetDbContext().AddParameter("@RegistreeID", userId);
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().GroupCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return list;
        }
        public List<Group> GetByInstructorID(string instructorID)
        {
            List<Group> list = new List<Group>();
            string sql = @"SELECT Groups.GroupID, Groups.GroupName, Groups.InstructorID, Groups.Room FROM Groups
                           WHERE Groups.InstructorID=@InstructorID";
            GetDbContext().AddParameter("@InstructorID", instructorID);
            using (IDataReader reader= GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().GroupCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return list;
        }

        public Group GetById(string id)
        {
            string sql = "SELECT * FROM Groups WHERE GroupID=@GroupId";
            GetDbContext().AddParameter("@GroupId", id);

            IDataReader reader = GetDbContext().ReadValue(sql);
            Group res = null;
            while (reader.Read())
            {
                res = GetModelFactory().GroupCreator.CreateModel(reader);
            }
            reader.Dispose();
            GetDbContext().ClearParameters();
            return res;
        }

        public bool Update(Group entity)
        {
            string sql = @"UPDATE Groups 
                        SET GroupName=@GroupName,InstructorID=@InstructorID,Room=@Room
                        WHERE GroupID=@GroupId";
            GetDbContext().AddParameter("@GroupId", entity.Id);
            GetDbContext().AddParameter("@GroupName", entity.Name);
            GetDbContext().AddParameter("@InstructorID", entity.Id);
            GetDbContext().AddParameter("@Room", entity.Room);
            int res = GetDbContext().Update(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
        public bool Update(Group entity, Instructor instructor)
        {
            string sql = @"UPDATE Groups 
                        SET GroupName=@GroupName,InstructorID=@InstructorID,Room=@Room
                        WHERE GroupID=@GroupId";
            GetDbContext()
                .AddParameter("@GroupName", entity.Name)
                .AddParameter("@InstructorID", instructor.Id)
                .AddParameter("@Room", entity.Room)
                .AddParameter("@GroupId", entity.Id);
            int res = GetDbContext().Update(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public bool UpdateInstructor(string groupID, string instructorID)
        {
            string sql = @"UPDATE Groups 
                        SET InstructorID=@InstructorID
                        WHERE GroupID=@GroupId";
            GetDbContext()
                .AddParameter("@InstructorID", instructorID)
                .AddParameter("@GroupId", groupID);
            int res = GetDbContext().Update(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
    }
}
