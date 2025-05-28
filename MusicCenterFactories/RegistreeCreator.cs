using System.Data;
using System.Security.Cryptography.X509Certificates;
using MusicCenterModels;

namespace MusicCenterFactories;

public class RegistreeCreator: IModelCreator<Registree>
{
    public Registree CreateModel(IDataReader src)
    {
        Registree model = new Registree()
        {
            Id = Convert.ToString(src["RegistreeID"]),
            Name = Convert.ToString(src["username"]),
            Password = Convert.ToString(src["password"]),
            Email = Convert.ToString(src["email"]),
            PhoneNumber = Convert.ToString(src["phoneNumber"]),
            Address = Convert.ToString(src["address"]),
            Image = Convert.ToString(src["Image"]),
            ValidationKey = Convert.ToString(src["ValidationKey"]),
            RequestsSent = null,
            RequestsRecieved = null,
            Messages = null,
            Lessons = null,
            Groups = null,
            Teachers = null
        };
        return model;
    } 
}