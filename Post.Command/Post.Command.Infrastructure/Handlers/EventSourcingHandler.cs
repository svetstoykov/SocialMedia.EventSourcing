using Post.Command.Domain.Aggregates;
using SocialMedia.EventSourcing.Domain;
using SocialMedia.EventSourcing.Handlers;
using SocialMedia.EventSourcing.Infrastructure;

namespace Post.Command.Infrastructure.Handlers;

public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
{
    private readonly IEventStore _eventStore;

    public EventSourcingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task SaveAsync(AggregateRoot aggregateRoot)
    {
        await _eventStore.SaveEventsAsync(
            aggregateRoot.Id, aggregateRoot.GetUncommittedChanges(), aggregateRoot.Version);
        
        aggregateRoot.MarkChangesAsCommitted();
    }

    public async Task<PostAggregate> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new PostAggregate();
        var events = await _eventStore.GetEventsAsync(aggregateId);

        aggregate.ReplayEvents(events);

        aggregate.Version = events
            .Select(e => e.Version)
            .Max();

        return aggregate;
    }
}