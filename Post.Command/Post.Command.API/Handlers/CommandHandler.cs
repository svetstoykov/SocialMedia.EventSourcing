using Post.Command.API.Commands.Comments;
using Post.Command.API.Commands.Posts;
using Post.Command.API.Handlers.Interfaces;
using Post.Command.Domain.Aggregates;
using SocialMedia.EventSourcing.Handlers;

namespace Post.Command.API.Handlers;

public class CommandHandler : ICommandHandler
{
    private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

    public CommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task HandleAsync(CreatePostCommand command)
    {
        var aggregate = new PostAggregate(command.Id, command.Author, command.Message);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(EditPostCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);

        aggregate.EditPost(command.Message);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(DeletePostCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);

        aggregate.DeletePost(command.Username);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(LikePostCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);

        aggregate.LikePost();

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(AddCommentCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);

        aggregate.AddComment(command.Comment, command.Username);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(EditCommentCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);

        aggregate.EditComment(command.CommentId, command.Comment, command.Username);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(RemoveCommentCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);

        aggregate.RemoveComment(command.CommentId, command.Username);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }
}