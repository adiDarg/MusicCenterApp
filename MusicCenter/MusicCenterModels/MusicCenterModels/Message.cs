namespace MusicCenterModels;

public class Message
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<User>? Receivers { get; set; }
}