using System.Data;
using MusicCenterModels;

namespace MusicCenterFactories;

public class InstructorCreator: IModelCreator<Instructor>
{
    public Instructor CreateModel(IDataReader src)
    {
        Instructor model = new Instructor()
        {
            Id = Convert.ToString(src["InstructorID"]),
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
            Groups = new List<Group>(),
            Lessons = new List<Lesson>(),
            Students = new List<Registree>(),
            Instruments = new List<Instrument>()
        };
        return model;
    }
}