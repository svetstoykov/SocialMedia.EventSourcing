using System.Runtime.InteropServices.JavaScript;
using SocialMedia.EventSourcing.Events;

namespace Post.Common.Events.Comments;

public class CommentRemovedEvent : BaseEvent
{
    public CommentRemovedEvent() : base(nameof(CommentRemovedEvent))
    {
    }
    
    public Guid CommentId { get; set; }
}