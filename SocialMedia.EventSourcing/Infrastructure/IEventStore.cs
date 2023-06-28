using SocialMedia.EventSourcing.Events;

namespace SocialMedia.EventSourcing.Infrastructure;

public interface IEventStore
{
    Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion);

    Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId);
}