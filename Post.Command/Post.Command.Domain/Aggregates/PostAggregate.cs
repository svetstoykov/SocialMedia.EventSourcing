using Post.Common.Events.Comments;
using Post.Common.Events.Posts;
using SocialMedia.EventSourcing.Domain;

namespace Post.Command.Domain.Aggregates;

public class PostAggregate : AggregateRoot
{
    private bool _active;

    private string _author;

    private readonly Dictionary<Guid, (string, string)> _comments = new();

    public bool Active
    {
        get => _active;
        set => _active = value;
    }

    public PostAggregate()
    {
    }

    public PostAggregate(Guid id, string author, string message)
    {
        RaiseEvent(new PostCreatedEvent
        {
            Id = id,
            Author = author,
            Message = message,
            DatePosted = DateTime.UtcNow
        });
    }

    public void Apply(PostCreatedEvent @event)
    {
        _id = @event.Id;
        _active = true;
        _author = @event.Author;
    }

    public void EditPost(string message)
    {
        if (!_active)
        {
            throw new InvalidOperationException("You cannot edit the message of an inactive post");
        }

        if (string.IsNullOrEmpty(message))
        {
            throw new InvalidOperationException("Message cannot be empty!");
        }

        RaiseEvent(new PostEditedEvent {Id = _id, Message = message});
    }

    public void Apply(PostEditedEvent @event)
    {
        _id = @event.Id;
    }

    public void LikePost()
    {
        if (!_active)
        {
            throw new InvalidOperationException("You cannot like an inactive post");
        }

        RaiseEvent(new PostLikedEvent {Id = _id});
    }

    public void Apply(PostLikedEvent @event)
    {
        _id = @event.Id;
    }

    public void DeletePost(string username)
    {
        if (!_active)
        {
            throw new InvalidOperationException("You cannot delete an inactive post");
        }

        if (_author != username)
        {
            throw new InvalidOperationException("You cannot delete someone else's post!");
        }
        
        RaiseEvent(new PostDeletedEvent()
        {
            Id = _id
        });
    }

    public void Apply(PostDeletedEvent @event)
    {
        _id = @event.Id;
        _active = false;
    }

    public void AddComment(string comment, string username)
    {
        if (!_active)
        {
            throw new InvalidOperationException("You cannot add a comment on an inactive post");
        }

        if (string.IsNullOrEmpty(comment))
        {
            throw new InvalidOperationException("Comment cannot be empty!");
        }

        RaiseEvent(new CommentAddedEvent()
        {
            Id = _id,
            CommentId = Guid.NewGuid(),
            Comment = comment,
            Username = username,
            CommentDate = DateTime.UtcNow
        });
    }

    public void Apply(CommentAddedEvent @event)
    {
        _id = @event.Id;
        _comments.Add(@event.CommentId, (@event.Comment, @event.Username));
    }

    public void EditComment(Guid commentId, string comment, string username)
    {
        if (!_active)
        {
            throw new InvalidOperationException("You cannot edit the comment of an inactive post");
        }

        if (string.IsNullOrEmpty(comment))
        {
            throw new InvalidOperationException("Comment cannot be empty!");
        }

        if (!_comments.TryGetValue(commentId, out var commentUser) || commentUser.Item2 != username)
        {
            throw new InvalidOperationException("Invalid comment or username");
        }

        RaiseEvent(new CommentEditedEvent
        {
            Id = _id,
            CommentId = commentId,
            Comment = comment,
            Username = username,
            EditDate = DateTime.UtcNow
        });
    }

    public void Apply(CommentEditedEvent @event)
    {
        _id = @event.Id;
        _comments[@event.CommentId] = (@event.Comment, @event.Username);
    }

    public void RemoveComment(Guid commentId, string username)
    {
        if (!_active)
        {
            throw new InvalidOperationException("You cannot remove the comment of an inactive post");
        }

        if (!_comments.TryGetValue(commentId, out var commentUser) || commentUser.Item2 != username)
        {
            throw new InvalidOperationException("Invalid comment or username");
        }
        
        RaiseEvent(new CommentRemovedEvent
        {
            Id = _id,
            CommentId = commentId,
        });
    }
    
    public void Apply(CommentRemovedEvent @event)
    {
        _id = @event.Id;
        _comments.Remove(@event.CommentId);
    }
}