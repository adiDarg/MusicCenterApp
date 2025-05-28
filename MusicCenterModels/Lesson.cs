namespace MusicCenterModels;

public class Lesson: McEvent
{
    public Registree? Student {get; set;}
    public Teacher? Teacher {get; set;}
}