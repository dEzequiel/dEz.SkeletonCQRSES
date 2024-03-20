using dEz.SkeletonCQRSES.Command.Infrastructure.Configuration;
using dEz.SkeletonCQRSES.ES.Core.Domain;
using dEz.SkeletonCQRSES.ES.Core.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Command.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for events-related operations.
    /// </summary>
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoCollection<EventModel> _eventStoreCollection;
        private readonly MongoSettings _settings;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="config"></param>
        public EventStoreRepository(IOptions<MongoSettings> config)
        {
            _settings = config.Value;
            var mongoClient = new MongoClient(_settings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_settings.Database);
            _eventStoreCollection = mongoDatabase.GetCollection<EventModel>(_settings.Collection);
        }

        ///<inheritdoc cref="IEventStoreRepository"/>
        public async Task<IEnumerable<EventModel>> FindByAggregateId(Guid aggregateId) =>
            await _eventStoreCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync().ConfigureAwait(false);

        ///<inheritdoc cref="IEventStoreRepository"/>
        public async Task SaveAsync(EventModel @event) =>
            await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);

        ///<inheritdoc cref="IEventStoreRepository"/>
        public async Task<IEnumerable<EventModel>> FindAllAsync() =>
            await _eventStoreCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
    }
}
