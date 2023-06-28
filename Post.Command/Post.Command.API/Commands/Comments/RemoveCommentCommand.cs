using SocialMedia.EventSourcing.Commands;

namespace Post.Command.API.Commands.Comments;

public class RemoveCommentCommand : BaseCommand
{
    public Guid CommentId { get; set; }

    public string Username { get; set; }
}