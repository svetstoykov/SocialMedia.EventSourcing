using SocialMedia.EventSourcing.Commands;

namespace Post.Command.API.Commands.Comments;

public class AddCommentCommand : BaseCommand
{
    public string Comment { get; set; }
    
    public string Username { get; set; }
}