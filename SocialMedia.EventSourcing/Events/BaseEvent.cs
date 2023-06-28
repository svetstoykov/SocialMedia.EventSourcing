using SocialMedia.EventSourcing.Messages;

namespace SocialMedia.EventSourcing.Events;

public class BaseEvent : Message
{
    protected BaseEvent(string type)
    {
        Type = type;
    }
    
    public int Version { get; set; }
    
    public string Type { get; set; }
}