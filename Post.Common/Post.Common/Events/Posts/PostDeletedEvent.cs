using SocialMedia.EventSourcing.Events;

namespace Post.Common.Events.Posts;

public class PostDeletedEvent : BaseEvent
{
    public PostDeletedEvent() : base(nameof(PostDeletedEvent))
    {
    }
}