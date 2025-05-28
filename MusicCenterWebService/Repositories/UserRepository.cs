using MusicCenterFactories;
using MusicCenterModels;
using System.Data;

namespace MusicCenterWebService.Repositories
{
    public class UserRepository : Repository, IRepository<User>
    {
        public bool Create(User entity)
        {
            string sql = @"INSERT INTO Users([username], [password], [mail], adress, phoneNumber, [Image], ValidationKey) 
                           VALUES(@Username, @Password, @Mail, @Adress, @PhoneNumber, @Image, @ValidationKey)";
            GetDbContext()
                .AddParameter("@Username", entity.Name)
                .AddParameter("@Password", entity.Password)
                .AddParameter("@Mail", entity.Email)
                .AddParameter("@Adress", entity.Address)
                .AddParameter("@PhoneNumber", entity.PhoneNumber)
                .AddParameter("@Image", entity.Image)
                .AddParameter("@ValidationKey", entity.ValidationKey);
            int res = GetDbContext().Create(sql);
            return res > 0;
        }

        public bool Delete(User entity)
        {
            string sql = "DELETE FROM Users WHERE UserID=@UserId";
            GetDbContext().AddParameter("@UserId", entity.Id);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
        public List<User> GetAll()
        {
            List<User> list = new List<User>();
            string sql = "SELECT * FROM Users";
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().UserCreator.CreateModel(reader));
                }
            }
            return list;
        }

        public User GetById(string id)
        {
            string sql = "SELECT * FROM Users WHERE UserID=@UserId";
            GetDbContext().AddParameter("@UserId", id);
            IDataReader reader = GetDbContext().ReadValue(sql);
            User res = null;
            while (reader.Read())
            {
                res = GetModelFactory().UserCreator.CreateModel(reader);
            }
            reader.Dispose();
            GetDbContext().ClearParameters();
            return res;
        }
        public User GetSenderByRequestID(string requestID)
        {
            string sql = @"SELECT [UserID], [username], [password], [mail], phoneNumber, adress, [Image], ValidationKey
                        FROM Users u, Requests r WHERE u.UserID=r.SenderID AND r.RequestID=@RequestID";
            GetDbContext().AddParameter("@RequestID", requestID);
            User res = null;
            using (IDataReader reader = GetDbContext().Read(sql))
            {

                if (reader.Read())
                {
                    res = GetModelFactory().UserCreator.CreateModel(reader);
                }
            }
            GetDbContext().ClearParameters();
            return res;
        }
        public User GetRecieverByRequestID(string requestID) {
            string sql = @"SELECT [UserID], [username], [password], [mail], phoneNumber, adress, [Image], ValidationKey
                        FROM Users u, Requests r WHERE u.UserID=r.RecieverID AND r.RequestID=@RequestID";
            GetDbContext().AddParameter("@RequestID", requestID);
            User res = null;
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                if (reader.Read())
                {
                    res = GetModelFactory().UserCreator.CreateModel(reader);
                }
            }
            GetDbContext().ClearParameters();
            return res;
        }
        public User GetByUsername(string username)
        {
            string sql = "SELECT * FROM Users WHERE [Username]=@Username";
            GetDbContext().AddParameter("@Username", username);
            IDataReader reader = GetDbContext().Read(sql);
            User res = null;
            try
            {
                while (reader.Read())
                {
                    res = GetModelFactory().UserCreator.CreateModel(reader);
                }
            }
            catch (Exception ex) {
                return null;
            }
            finally
            {
                reader.Close();
            }
            GetDbContext().ClearParameters();
            return res;
        }

        public bool Update(User entity)
        {
            string sql = @"UPDATE Users 
                        SET [Username]=@Username, [Password]=@Password, [Mail]=@Mail, Adress=@Adress, 
                            PhoneNumber=@PhoneNumber, [Image]=@Image, ValidationKey=@ValidationKey
                        WHERE UserID=@UserId";
            GetDbContext()
                .AddParameter("@Username", entity.Name)
                .AddParameter("@Password", entity.Password)
                .AddParameter("@Mail", entity.Email)
                .AddParameter("@Adress", entity.Address)
                .AddParameter("@PhoneNumber", entity.PhoneNumber)
                .AddParameter("@Image", entity.Image)
                .AddParameter("@ValidationKey", entity.ValidationKey)
                .AddParameter("@UserId", entity.Id);
            int res = GetDbContext().Update(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
    }
}
