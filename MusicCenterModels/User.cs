namespace MusicCenterModels;

public class User: Model
{
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Image { get; set; }
    public string? ValidationKey { get; set; }
    public List<Request>? RequestsSent { get; set; }
    public List<Request>? RequestsRecieved { get; set; }
    public List<Message>? Messages { get; set; }

    public bool Equals(User other)
    {
        bool equals = Name.Equals(other.Name);
        equals &= Password.Equals(other.Password);
        equals &= Email.Equals(other.Email);
        equals &= PhoneNumber.Equals(other.PhoneNumber);
        equals &= Address.Equals(other.Address);
        equals &= Image.Equals(other.Image);
        return equals;
    }
}