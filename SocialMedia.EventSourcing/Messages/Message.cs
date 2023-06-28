namespace SocialMedia.EventSourcing.Messages;

public abstract class Message
{
    public Guid Id { get; set; }
}