using SocialMedia.EventSourcing.Commands;

namespace Post.Command.API.Commands.Posts;

public class CreatePostCommand : BaseCommand
{
    public string Author { get; set; }
    
    public string Message { get; set; }
}