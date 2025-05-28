using MusicCenterFactories;
using MusicCenterModels;
using System.Collections.Generic;
using System.Data;

namespace MusicCenterWebService.Repositories
{
    public class RegistreeRepository : Repository, IRepository<Registree>
    {
        public bool Create(Registree entity)
        {
            string sql = @"INSERT INTO Registrees(RegistreeID)
                         values(@RegistreeId)";
            GetDbContext().AddParameter("@RegistreeId", entity.Id);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
        public bool Delete(Registree entity)
        {
            string sql = "DELETE FROM Registrees WHERE RegistreeID=@RegistreeId";
            GetDbContext().AddParameter("@RegistreeId", entity.Id);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public List<Registree> GetAll()
        {
            List<Registree> list = new List<Registree>();
            string sql = @"SELECT RegistreeID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey 
                           FROM Registrees r, Users u 
                           WHERE r.RegistreeID = u.UserID";
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().RegistreeCreator.CreateModel(reader));
                }
            }
            return list;
        }

        public Registree GetById(string id)
        {
            string sql = @"SELECT RegistreeID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey 
                           FROM Registrees r, Users u 
                           WHERE r.RegistreeID = u.UserID AND RegistreeID=@RegistreeId";
            GetDbContext().AddParameter("@RegistreeId", id);
            IDataReader reader = GetDbContext().ReadValue(sql);
            Registree res = null;
            while (reader.Read())
            {
                res = GetModelFactory().RegistreeCreator.CreateModel(reader);
            }
            reader.Dispose();
            GetDbContext().ClearParameters();
            return res;
        }
        public List<Registree> GetAllByGroupId(string groupID)
        {
            string sql = @"SELECT r.RegistreeID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey 
                           FROM Registrees r, Users u, RegistreesGroups rg
                           WHERE r.RegistreeID = u.UserID AND rg.groupID=@groupID AND rg.RegistreeID= r.RegistreeID";
            GetDbContext().AddParameter("@groupID", groupID);

            List<Registree> res = new List<Registree>();

            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    res.Add(GetModelFactory().RegistreeCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return res;
        }
        public Registree GetByLessonID(string lessonID) {
            string sql = @"SELECT r.registreeID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey   
                           FROM Registrees r, Users u, Lessons l               
                           WHERE l.RegistreeID = r.RegistreeID AND r.RegistreeID = u.UserID AND l.LessonID=@LessonID;";
            GetDbContext().AddParameter("@LessonID", lessonID);
            Registree result = null;
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                if (reader.Read())
                {
                    result = GetModelFactory().RegistreeCreator.CreateModel(reader);
                }
            }
            GetDbContext().ClearParameters();
            return result;
        }

        public bool Update(Registree entity)
        {
            return true;
        }
    }
}
