using System.Data;
using MusicCenterModels;

namespace MusicCenterFactories;

public class MessageCreator: IModelCreator<Message>
{
    public Message CreateModel(IDataReader src)
    {
        Message model = new Message()
        {
            Id = Convert.ToString(src["MessageID"]),
            Title = Convert.ToString(src["Title"]),
            Description = Convert.ToString(src["Description"]),
            Receivers = new List<User>()
        };
        return model;
    }
}