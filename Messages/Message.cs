namespace Messages;

public class Message
{
    public Dictionary<string, object> Headers { get; } = new();
    public string? Text { get; set; }
    
    public Message()
    { }

    public Message(string text)
    {
        Text = text;
    }
}