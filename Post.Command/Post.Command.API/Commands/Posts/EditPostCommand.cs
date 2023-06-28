using SocialMedia.EventSourcing.Commands;

namespace Post.Command.API.Commands.Posts;

public class EditPostCommand : BaseCommand
{
    public string Message { get; set; }
}