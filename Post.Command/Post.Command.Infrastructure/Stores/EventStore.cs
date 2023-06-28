using Post.Command.Domain.Aggregates;
using SocialMedia.EventSourcing.Events;
using SocialMedia.EventSourcing.Infrastructure;

namespace Post.Command.Infrastructure.Stores;

public class EventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public EventStore(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventStream = await GetEventStreamByAggregateIdAsync(aggregateId);

        if (expectedVersion == -1 && eventStream[^1].Version != expectedVersion)
        {
            throw new InvalidOperationException("Invalid event model version");
        }

        var version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            @event.Version = version;

            var eventModel = new EventModel()
            {
                TimeStamp = DateTime.UtcNow,
                AggregateIdentifier = aggregateId,
                AggregateType = nameof(PostAggregate),
                Version = version,
                EventType = @event.GetType().Name,
                EventData = @event
            };

            await _eventStoreRepository.AddAsync(eventModel);
        }
    }
    
    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
    {
        var eventStream = await GetEventStreamByAggregateIdAsync(aggregateId);

        return eventStream
            .OrderBy(v => v.Version)
            .Select(e => e.EventData)
            .ToList();
    }
    
    private async Task<List<EventModel>> GetEventStreamByAggregateIdAsync(Guid aggregateId)
    {
        var eventsStream = await _eventStoreRepository.FindByAggregateIdAsync(aggregateId);

        if (eventsStream == null || !eventsStream.Any())
        {
            throw new ArgumentNullException("Invalid post ID provided!");
        }

        return eventsStream;
    }
}