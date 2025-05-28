using System.Data;
using MusicCenterModels;

namespace MusicCenterFactories;

public class RequestCreator: IModelCreator<Request>
{
    public Request CreateModel(IDataReader src)
    {
        Request model = new Request()
        {
            Id = Convert.ToString(src["RequestID"]),
            Title = Convert.ToString(src["Title"]),
            Description = Convert.ToString(src["Description"]),
            IsSeen = Convert.ToBoolean(src["IsSeen"]),
            IsApproved = Convert.ToBoolean(src["IsApproved"]),
            Sender = null,
            Reciever = null,
            RequestType = Convert.ToString(src["RequestType"])
        };
        return model;
    }
}