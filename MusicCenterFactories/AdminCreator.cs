using System.Data;
using MusicCenterModels;

namespace MusicCenterFactories;

public class AdminCreator: IModelCreator<Admin>
{
    public Admin CreateModel(IDataReader src)
    {
        Admin model = new Admin()
        {
            Id = Convert.ToString(src["AdminID"]),
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
        };
        return model;
    }
}