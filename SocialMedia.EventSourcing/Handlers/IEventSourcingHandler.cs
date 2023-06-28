using SocialMedia.EventSourcing.Domain;

namespace SocialMedia.EventSourcing.Handlers;

public interface IEventSourcingHandler<T> where T : AggregateRoot
{
    Task SaveAsync(AggregateRoot aggregateRoot);

    Task<T> GetByIdAsync(Guid aggregateId);
}