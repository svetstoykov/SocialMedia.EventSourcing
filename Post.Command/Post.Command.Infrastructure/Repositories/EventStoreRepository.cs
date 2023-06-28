using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Post.Command.Infrastructure.Settings;
using SocialMedia.EventSourcing.Events;
using SocialMedia.EventSourcing.Infrastructure;

namespace Post.Command.Infrastructure.Repositories;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly IMongoCollection<EventModel> _eventModelStore;

    public EventStoreRepository(
        IMongoClient mongoClient,
        IOptions<MongoDbConfig> mongoDbConfig)
    {
        _eventModelStore = mongoClient
            .GetDatabase(mongoDbConfig.Value.Database)
            .GetCollection<EventModel>(mongoDbConfig.Value.Collection);
    }
    
    public async Task AddAsync(EventModel eventModel) 
        => await _eventModelStore.InsertOneAsync(eventModel);

    public async Task<List<EventModel>> FindByAggregateIdAsync(Guid aggregateId) 
        => await _eventModelStore.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync();
}