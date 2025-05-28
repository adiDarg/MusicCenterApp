using System.Data;
using System.Net;
using System.Xml.Linq;
using MusicCenterModels;
using static System.Net.Mime.MediaTypeNames;

namespace MusicCenterFactories;

public class UserCreator: IModelCreator<User>
{
    public User CreateModel(IDataReader src)
    {
        User model = new User() 
        {
            Id = Convert.ToString(src["UserID"]), 
            Name = Convert.ToString(src["username"]),
            Password = Convert.ToString(src["password"]),
            Email = Convert.ToString(src["mail"]),
            PhoneNumber = Convert.ToString(src["phoneNumber"]),
            Address = Convert.ToString(src["adress"]),
            Image = Convert.ToString(src["Image"]),
            ValidationKey = Convert.ToString(src["ValidationKey"]),
            RequestsSent = null,
            RequestsRecieved = null,
            Messages = null,
        };
        return model;
    }
}