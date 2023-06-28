using SocialMedia.EventSourcing.Events;

namespace Post.Common.Events.Posts;

public class PostEditedEvent : BaseEvent
{
    public PostEditedEvent() : base(nameof(PostEditedEvent))
    {
    }
    
    public string Message { get; set; }
}