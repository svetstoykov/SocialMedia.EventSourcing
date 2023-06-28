using SocialMedia.EventSourcing.Events;

namespace Post.Common.Events.Posts;

public class PostLikedEvent : BaseEvent
{
    public PostLikedEvent() : base(nameof(PostLikedEvent))
    {
    }
}