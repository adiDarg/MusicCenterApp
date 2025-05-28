using MusicCenterFactories;
using MusicCenterModels;
using System.Data;

namespace MusicCenterWebService.Repositories
{
    public class AdminRepository : Repository, IRepository<Admin>
    {
        public bool Create(Admin entity)
        {
            string sql = @"INSERT INTO Admins(AdminID)
                         values(@AdminId)";
            GetDbContext().AddParameter("@AdminId",entity.Id);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
        public bool Delete(Admin entity)
        {
            string sql = "DELETE FROM Admins WHERE AdminID=@AdminId";
            GetDbContext().AddParameter("@AdminId",entity.Id);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public List<Admin> GetAll()
        {
            List<Admin> list = new List<Admin>();
            string sql = @"SELECT AdminID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey 
                           FROM Admins a, Users u 
                           WHERE a.AdminID = u.UserID";

            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().AdminCreator.CreateModel(reader));
                }
            }

            return list;
        }

        public Admin GetById(string id)
        {
            string sql = @"SELECT AdminID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey 
                           FROM Admins a, Users u 
                           WHERE a.AdminID = u.UserID AND AdminID=@AdminId";

            GetDbContext().AddParameter("@AdminId", id);

            Admin res = null;

            using (IDataReader reader = GetDbContext().ReadValue(sql))
            {
                if (reader.Read())
                {
                    res = GetModelFactory().AdminCreator.CreateModel(reader);
                }
            }

            GetDbContext().ClearParameters();
            return res;
        }

        public bool Update(Admin entity)
        {
            return true;
        }
    }
}
