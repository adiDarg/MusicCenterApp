namespace MusicCenterModels;

public class Lesson: Model
{
    public string? LessonDate {get; set;}
    public Registree? Student {get; set;}
    public Teacher? Teacher {get; set;}
    public string? Room {get; set;}
}