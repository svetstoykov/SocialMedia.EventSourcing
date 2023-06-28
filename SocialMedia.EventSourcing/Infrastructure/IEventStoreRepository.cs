using SocialMedia.EventSourcing.Events;

namespace SocialMedia.EventSourcing.Infrastructure;

public interface IEventStoreRepository
{
    Task AddAsync(EventModel eventModel);

    Task<List<EventModel>> FindByAggregateIdAsync(Guid aggregateId);
}