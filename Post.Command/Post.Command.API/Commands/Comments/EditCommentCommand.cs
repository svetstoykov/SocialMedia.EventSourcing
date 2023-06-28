using SocialMedia.EventSourcing.Commands;

namespace Post.Command.API.Commands.Comments;

public class EditCommentCommand : BaseCommand
{
    public Guid CommentId { get; set; }
    
    public string Comment { get; set; }
    
    public string Username { get; set; }
}