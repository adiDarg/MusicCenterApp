using MusicCenterFactories;
using MusicCenterModels;
using System.Data;

namespace MusicCenterWebService.Repositories
{
    public class TeacherRepository : Repository, IRepository<Teacher>
    {
        public bool Create(Teacher entity)
        {
            string sql = @"INSERT INTO Teachers(TeacherID)
                         values(@TeacherId)";
            GetDbContext().AddParameter("@TeacherId", entity.Id);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
        public bool AddInstrument(string teacherID, string instrumentID) {
            string sql = @"INSERT INTO TeachersInstruments(TeacherID,InstrumentID)
                           VALUES(@TeacherID, @InstrumentID)";
            GetDbContext().AddParameter("@TeacherID", teacherID)
                .AddParameter("@InstrumentID", instrumentID);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
        public bool Delete(Teacher entity)
        {
            string sql = "DELETE FROM Teachers WHERE TeacherID=@TeacherId";
            GetDbContext().AddParameter("@TeacherId", entity.Id);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public List<Teacher> GetAll()
        {
            List<Teacher> list = new List<Teacher>();
            string sql = @"SELECT TeacherID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey 
                           FROM Teachers t, Users u 
                           WHERE t.TeacherID = u.UserID";

            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().TeacherCreator.CreateModel(reader));
                }
            }
            return list;
        }
        public List<Teacher> GetByRegistreeID(string registreeID)
        {
            List<Teacher> list = new List<Teacher>();
            string sql = $@"SELECT t.TeacherID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey    
                           FROM TeachersRegistrees tr, Teachers t, Users u
                           WHERE tr.RegistreeID = @RegistreeID AND tr.TeacherID = t.TeacherID AND t.teacherID = u.UserID";
            GetDbContext().AddParameter("@RegistreeID", registreeID);
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().TeacherCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return list;
        }

        public Teacher GetByLessonID(string lessonID)
        {
            string sql = @"SELECT t.TeacherID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey 
                           FROM Teachers t, Users u, Lessons l
                           WHERE l.TeacherID = t.TeacherID AND t.TeacherID = u.UserID AND l.LessonID=@LessonID";
            GetDbContext().AddParameter("@LessonID", lessonID);
            Teacher result = null;
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                if (reader.Read())
                {
                    result = GetModelFactory().TeacherCreator.CreateModel(reader);
                }
            }
            GetDbContext().ClearParameters();
            return result;
        }

        public Teacher GetById(string id)
        {
            string sql = @"SELECT TeacherID, [username], [password], [mail] as [email], phoneNumber, adress as [address], [Image], ValidationKey 
                           FROM Teachers t, Users u 
                           WHERE t.TeacherID = u.UserID AND TeacherID=@TeacherId";

            GetDbContext().AddParameter("@TeacherId", id);
            IDataReader reader = GetDbContext().ReadValue(sql);
            Teacher res = null;
            while (reader.Read())
            {
                res = GetModelFactory().TeacherCreator.CreateModel(reader);
            }
            reader.Dispose();
            GetDbContext().ClearParameters();

            return res;
        }

        public bool Update(Teacher entity)
        {
            return true;
        }
    }
}
