namespace MusicCenterModels;

public class Meeting: Model
{
    public Group? Group { get; set; }
    public string? MeetingDate { get; set; }
    public string? Room { get; set; }
}