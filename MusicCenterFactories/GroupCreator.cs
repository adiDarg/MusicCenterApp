using System.Data;
using MusicCenterModels;

namespace MusicCenterFactories;

public class GroupCreator: IModelCreator<Group>
{
    public Group CreateModel(IDataReader src)
    {
        Group model = new Group()
        {
            Id = Convert.ToString(src["GroupID"]),
            Name = Convert.ToString(src["GroupName"]),
            Instructor = null,
            Room = Convert.ToString(src["Room"]),
            Meetings = new List<Meeting>(),
            Participants = new List<Registree>()
        };
        return model;
    }
}