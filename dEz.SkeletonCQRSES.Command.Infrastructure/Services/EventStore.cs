using dEz.SkeletonCQRSES.ES.Core.Domain;
using dEz.SkeletonCQRSES.ES.Core.Events;
using dEz.SkeletonCQRSES.ES.Core.Exceptions;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Command.Infrastructure.Services
{
    public class EventStore : IEventStore
    {
        //private readonly IEventProducer _eventProducer;
        private readonly IEventStoreRepository _eventStoreRepository;
        //private readonly ILoggerManager _logger;

        //public EventStore(IEventProducer eventProducer, IEventStoreRepository eventStoreRepository, ILoggerManager logger)
        //{
        //    _eventStoreRepository = eventStoreRepository;
        //    _logger = logger;
        //    _eventProducer = eventProducer;
        //}

        public EventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        /// <inheritdoc cref="IEventStore"/>
        public async Task<IEnumerable<BaseEvent>> GetEventsAsync(Guid aggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

            if (eventStream is null || !eventStream.Any())
                throw new AggregateNotFoundException(aggregateId);

            return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
        }

        /// <inheritdoc cref="IEventStore"/>
        public async Task SaveEventAsync(Guid aggregateId, string aggregateType, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

            // Check if the expected version is specified and compare it with the version of the last event in the stream.
            // If they do not match, throw a ConcurrencyException.
            if (expectedVersion != -1 && eventStream.Last().Version != expectedVersion)
                throw new ConcurrencyException();

            var version = expectedVersion;

            foreach (var @event in events)
            {
                version++;
                @event.Version = version;
                var eventType = @event.GetType().Name;
                var eventModel = new EventModel
                {
                    TimeStamp = DateTime.Now,
                    AggregateIdentifier = aggregateId,
                    AggregateType = aggregateType,
                    Version = version,
                    EventType = eventType,
                    EventData = @event
                };

                await _eventStoreRepository.SaveAsync(eventModel);

                var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
                //await _eventProducer.ProduceAsync(topic, @event);
            }
        }

    }
}

