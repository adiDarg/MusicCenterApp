﻿namespace MusicCenterModels;

public class Request: Model
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsSeen { get; set; }
    public bool? IsApproved { get; set; }
    public User? Sender { get; set; }
    public User? Reciever { get; set; }
    public string? RequestType { get; set; }
}