﻿using System.Runtime.InteropServices.JavaScript;
using SocialMedia.EventSourcing.Events;

namespace Post.Common.Events.Comments;

public class CommentAddedEvent : BaseEvent
{
    public CommentAddedEvent() : base(nameof(CommentAddedEvent))
    {
    }
    
    public Guid CommentId { get; set; }
    
    public string Comment { get; set; }
    
    public string Username { get; set; }
    
    public DateTime CommentDate { get; set; }
}