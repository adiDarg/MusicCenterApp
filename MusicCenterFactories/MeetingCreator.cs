using System.Data;
using MusicCenterModels;

namespace MusicCenterFactories;

public class MeetingCreator: IModelCreator<Meeting>
{
    public Meeting CreateModel(IDataReader src)
    {
        Meeting model = new Meeting()
        {
            Id = Convert.ToString(src["MeetingID"]),
            Group = null,
            Date = Convert.ToString(src["MeetingDate"]),
            Room = Convert.ToString(src["Room"])
        };
        return model;
    }
}