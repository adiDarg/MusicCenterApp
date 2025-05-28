using System.Data;
using MusicCenterModels;

namespace MusicCenterFactories;

public class LessonCreator: IModelCreator<Lesson>
{
    public Lesson CreateModel(IDataReader src)
    {
        Lesson model = new Lesson()
        {
            Id = Convert.ToString(src["LessonID"]),
            Date = Convert.ToString(src["LessonDate"]),
            Student = null,
            Teacher = null,
            Room = Convert.ToString(src["Room"])
        };
        return model;
    }
}