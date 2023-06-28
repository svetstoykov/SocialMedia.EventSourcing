using Post.Command.API.Commands.Comments;
using Post.Command.API.Commands.Posts;

namespace Post.Command.API.Handlers.Interfaces;

public interface ICommandHandler
{
    Task HandleAsync(CreatePostCommand command);
    
    Task HandleAsync(EditPostCommand command);
    
    Task HandleAsync(DeletePostCommand command);
    
    Task HandleAsync(LikePostCommand command);
    
    Task HandleAsync(AddCommentCommand command);
    
    Task HandleAsync(EditCommentCommand command);
    
    Task HandleAsync(RemoveCommentCommand command);
}