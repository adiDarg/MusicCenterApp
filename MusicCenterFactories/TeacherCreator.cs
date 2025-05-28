using System.Data;
using MusicCenterModels;

namespace MusicCenterFactories;

public class TeacherCreator: IModelCreator<Teacher>
{
    public Teacher CreateModel(IDataReader src)
    {
        Teacher model = new Teacher()
        {
            Id = Convert.ToString(src["TeacherID"]),
            Name = Convert.ToString(src["username"]),
            Password = Convert.ToString(src["password"]),
            Email = Convert.ToString(src["email"]),
            PhoneNumber = Convert.ToString(src["phoneNumber"]),
            Address = Convert.ToString(src["address"]),
            Image = Convert.ToString(src["Image"]),
            ValidationKey = Convert.ToString(src["ValidationKey"]),
            RequestsSent = new List<Request>(),
            RequestsRecieved = new List<Request>(),
            Messages = new List<Message>(),
            Lessons = new List<Lesson>(),
            Students = new List<Registree>(),
            Instruments = new List<Instrument>()
        };
        return model;
    }
}