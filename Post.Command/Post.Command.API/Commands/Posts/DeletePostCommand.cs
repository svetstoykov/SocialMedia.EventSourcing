using SocialMedia.EventSourcing.Commands;

namespace Post.Command.API.Commands.Posts;

public class DeletePostCommand : BaseCommand
{
    public string Username { get; set; }
}