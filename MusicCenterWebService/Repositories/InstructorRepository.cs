using MusicCenterModels;
using System.Data;

namespace MusicCenterWebService.Repositories
{
    public class InstructorRepository : Repository, IRepository<Instructor>
    {
        public bool Create(Instructor entity)
        {
            string sql = @"INSERT INTO Instructors(InstructorID)
                         values(@InstructorId)";
            GetDbContext().AddParameter("@InstructorId",entity.Id);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public bool Delete(Instructor entity)
        {
            string sql = "DELETE FROM Instructors WHERE InstructorID=@InstructorId";
            GetDbContext().AddParameter("@InstructorId", entity.Id);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public List<Instructor> GetAll()
        {
            List<Instructor> list = new List<Instructor>();

            string sql = @"SELECT InstructorID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey 
                           FROM Instructors i, Users u 
                           WHERE i.InstructorID = u.UserID";

            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().InstructorCreator.CreateModel(reader));
                }
            }

            return list;
        }

        public Instructor GetById(string id)
        {
            string sql = @"SELECT InstructorID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey 
                           FROM Instructors i, Users u 
                           WHERE i.InstructorID = u.UserID AND InstructorID=@InstructorId";

            GetDbContext().AddParameter("@InstructorId", id);

            Instructor res = null;

            using (IDataReader reader = GetDbContext().Read(sql))
            {
                if (reader.Read())
                {
                    res = GetModelFactory().InstructorCreator.CreateModel(reader);
                }
            }

            GetDbContext().ClearParameters();
            return res;
        }
        public Instructor GetByGroupId(string groupID) {
            string sql = @"SELECT i.InstructorID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey 
                           FROM Instructors i, Users u, Groups g
                           WHERE i.InstructorID = u.UserID AND g.groupID=@groupID AND g.InstructorID= i.InstructorID";
            GetDbContext().AddParameter("@groupID", groupID);

            Instructor res = null;

            using (IDataReader reader = GetDbContext().Read(sql))
            {
                if (reader.Read())
                {
                    res = GetModelFactory().InstructorCreator.CreateModel(reader);
                }
            }
            GetDbContext().ClearParameters();
            return res;
        }

        public bool Update(Instructor entity)
        {
            return true;
        }
    }
}
