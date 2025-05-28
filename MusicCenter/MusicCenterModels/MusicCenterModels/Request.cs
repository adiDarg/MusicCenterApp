namespace MusicCenterModels;

public class Request
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsSeen { get; set; }
    public bool? IsApproved { get; set; }
    public User? Sender { get; set; }
}