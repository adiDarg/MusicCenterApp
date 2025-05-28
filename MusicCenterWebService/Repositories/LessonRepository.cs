using MusicCenterModels;
using System.Data;
using System.Data.Common;

namespace MusicCenterWebService.Repositories
{
    public class LessonRepository : Repository, IRepository<Lesson>
    {
        public bool Create(Lesson entity)
        {
            string sql = @"INSERT INTO Lessons(LessonDate,RegistreeID,TeacherID,Room)
                         values(@LessonDate,@RegistreeID,@InstructorID,@Room)";
            GetDbContext().ClearParameters();
            GetDbContext()
                .AddParameter("@LessonDate", entity.Date)
                .AddParameter("@RegistreeID", entity.Student.Id)
                .AddParameter("@InstructorID", entity.Teacher.Id)
                .AddParameter("@Room", entity.Room);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public bool Delete(Lesson entity)
        {
            string sql = "DELETE FROM Lessons WHERE LessonID=@LessonId";
            GetDbContext().AddParameter("@LessonId", entity.Id);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public List<Lesson> GetAll()
        {
            List<Lesson> list = new List<Lesson>();
            string sql = "SELECT * FROM Lessons";
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().LessonCreator.CreateModel(reader));
                }
            }
            return list;
        }
        public List<Lesson> GetByStudentID(string userID)
        {
            List<Lesson> list = new List<Lesson>();
            string sql = "SELECT * FROM Lessons WHERE RegistreeID=@RegistreeID";
            GetDbContext().AddParameter("@RegistreeID", userID);
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().LessonCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return list;
        }
        public List<Lesson> GetByTeacherID(string userID)
        {
            List<Lesson> list = new List<Lesson>();
            string sql = "SELECT * FROM Lessons WHERE TeacherID=@TeacherID";
            GetDbContext().AddParameter("@TeacherID", userID);
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().LessonCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return list;
        }

        public Lesson GetById(string id)
        {
            string sql = "SELECT * FROM Lessons WHERE LessonID=@LessonId";
            GetDbContext().AddParameter("@LessonId", id);
            IDataReader reader = GetDbContext().ReadValue(sql);
            Lesson res = null;
            while (reader.Read())
            {
                res = GetModelFactory().LessonCreator.CreateModel(reader);
            }
            reader.Dispose();
            GetDbContext().ClearParameters();
            return res;
        }

        public bool Update(Lesson entity)
        {
            string sql = @"UPDATE Lessons 
                          SET LessonDate=@LessonDate,RegistreeID=@RegistreeID,TeacherID=@InstructorID,Room=@Room
                          WHERE LessonID=@LessonId";
            GetDbContext()
                .AddParameter("@LessonDate", entity.Date)
                .AddParameter("@RegistreeID", entity.Student.Id)
                .AddParameter("@InstructorID", entity.Teacher.Id)
                .AddParameter("@Room", entity.Room)
                .AddParameter("@LessonId", entity.Id);
            int res = GetDbContext().Update(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
    }
}
