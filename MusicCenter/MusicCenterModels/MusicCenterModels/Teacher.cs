namespace MusicCenterModels;

public class Teacher: User
{
    public List<Lesson>? Lessons { get; set; }
    public List<Registree>? Students { get; set; }
    public List<Instrument>? Instruments { get; set; }
}