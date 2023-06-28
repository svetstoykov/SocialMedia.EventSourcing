using System.Runtime.InteropServices.JavaScript;
using SocialMedia.EventSourcing.Events;

namespace Post.Common.Events.Comments;

public class CommentEditedEvent : BaseEvent
{
    public CommentEditedEvent() : base(nameof(CommentEditedEvent))
    {
    }
    
    public Guid CommentId { get; set; }
    
    public string Comment { get; set; }
    
    public string Username { get; set; }
    
    public DateTime EditDate { get; set; }
}