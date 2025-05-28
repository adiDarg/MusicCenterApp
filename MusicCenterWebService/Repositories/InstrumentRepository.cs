using MusicCenterModels;
using System.Data;

namespace MusicCenterWebService.Repositories
{
    public class InstrumentRepository : Repository, IRepository<Instrument>
    {
        public bool Create(Instrument entity)
        {
            string sql = @"INSERT INTO Instruments(InstrumentName)
                         values(@InstrumentName)";
            GetDbContext().AddParameter("@InstrumentName", entity.Name);
            int res = GetDbContext().Create(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public bool Delete(Instrument entity)
        {
            string sql = "DELETE FROM Instruments WHERE InstrumentID=@InstrumentId";
            GetDbContext().AddParameter("@InstrumentId", entity.Id);
            int res = GetDbContext().Delete(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }

        public List<Instrument> GetAll()
        {
            List<Instrument> list = new List<Instrument>();
            string sql = "SELECT * FROM Instruments";
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    list.Add(GetModelFactory().InstrumentCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return list;
        }

        public Instrument GetById(string id)
        {
            string sql = "SELECT * FROM Instruments WHERE InstrumentID=@InstrumentId";
            GetDbContext().AddParameter("@InstrumentId", id);
            
            Instrument res = null;
            using (IDataReader reader = GetDbContext().ReadValue(sql))
            {
                if (reader.Read())
                {
                    res = GetModelFactory().InstrumentCreator.CreateModel(reader);
                }
            }
            GetDbContext().ClearParameters();
            return res;
        }
        public List<Instrument> GetByTeacherID(string teacherID)
        {
            string sql = @"SELECT i.InstrumentID, InstrumentName 
                           FROM Instruments i, TeachersInstruments ti
                           WHERE ti.TeacherID = @TeacherID AND ti.InstrumentID = i.InstrumentID";
            GetDbContext().AddParameter("@TeacherID", teacherID);
            List<Instrument> result = new List<Instrument>();
            using (IDataReader reader = GetDbContext().Read(sql))
            {
                while (reader.Read())
                {
                    result.Add(GetModelFactory().InstrumentCreator.CreateModel(reader));
                }
            }
            GetDbContext().ClearParameters();
            return result;
        }

        public bool Update(Instrument entity)
        {
            string sql = @"UPDATE Instruments SET InstrumentName=@InstrumentName
                            WHERE InstrumentID=@InstrumentID";
            GetDbContext()
                .AddParameter("@InstrumentName", entity.Name)
                .AddParameter("@InstrumentID", entity.Id);
            int res = GetDbContext().Update(sql);
            GetDbContext().ClearParameters();
            return res > 0;
        }
    }
}
