using Post.Command.API.Commands.Comments;
using Post.Command.API.Commands.Posts;
using Post.Command.API.Handlers.Interfaces;
using Post.Command.Infrastructure.Dispatchers;
using Post.Common.Events.Posts;
using SocialMedia.EventSourcing.Infrastructure;

namespace Post.Command.API.Extensions;

public static class ApiServicesRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var commandHandler = serviceProvider.GetRequiredService<ICommandHandler>();

        var dispatcher = new CommandDispatcher();
        dispatcher.RegisterHandler<CreatePostCommand>(commandHandler.HandleAsync);
        dispatcher.RegisterHandler<LikePostCommand>(commandHandler.HandleAsync);
        dispatcher.RegisterHandler<DeletePostCommand>(commandHandler.HandleAsync);
        dispatcher.RegisterHandler<EditPostCommand>(commandHandler.HandleAsync);
        dispatcher.RegisterHandler<AddCommentCommand>(commandHandler.HandleAsync);
        dispatcher.RegisterHandler<EditCommentCommand>(commandHandler.HandleAsync);
        dispatcher.RegisterHandler<RemoveCommentCommand>(commandHandler.HandleAsync);

        services.AddSingleton<ICommandDispatcher>(_ => dispatcher);
    }
}