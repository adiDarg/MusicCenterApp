namespace MusicCenterModels;

public class Registree: User
{
    public List<Lesson>? Lessons { get; set; }
    public List<Group>? Groups { get; set; }
    public List<Teacher>? Teachers { get; set; }
}