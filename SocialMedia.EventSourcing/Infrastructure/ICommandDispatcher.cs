using SocialMedia.EventSourcing.Commands;

namespace SocialMedia.EventSourcing.Infrastructure;

public interface ICommandDispatcher
{
    void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand;

    Task SendAsync(BaseCommand command);
}