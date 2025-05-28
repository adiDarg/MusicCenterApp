namespace MusicCenterModels;

public class Group: Model
{
    public string? Name { get; set; }
    public Instructor? Instructor { get; set; }
    public string? Room { get; set; }
    public List<Meeting>? Meetings { get; set; }
    public List<Registree>? Participants { get; set; }
}