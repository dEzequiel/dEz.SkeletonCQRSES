using dEz.SkeletonCQRSES.Command.Domain.Aggregates;
using dEz.SkeletonCQRSES.ES.Core.Domain;
using dEz.SkeletonCQRSES.ES.Core.Handlers;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Command.Infrastructure.Handlers
{
    public class EventSourcingHandler : IEventSourcingHandler<CompanyAggregate>
    {
        private readonly IEventStore _eventStore;

        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }


        /// <inheritdoc cref="IEventSourcingHandler{T}"/>
        public async Task<CompanyAggregate> GetByIdAsync(Guid aggregateId)
        {
            var aggregate = new CompanyAggregate();
            var events = await _eventStore.GetEventsAsync(aggregateId);

            if (events is null || !events.Any()) return aggregate;

            aggregate.ReplayEvents(events);
            aggregate.Version = events.Select(x => x.Version).Max();

            return aggregate;
        }

        /// <inheritdoc cref="IEventSourcingHandler{T}"/>
        public Task RepublishEventsAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="IEventSourcingHandler{T}"/>
        public async Task SaveAsync(AggregateRoot aggregate)
        {
            await _eventStore.SaveEventAsync(aggregate.Id, aggregate.GetType().Name, aggregate.GetUncommitedChanges(), aggregate.Version);
            aggregate.MarkChangesAsCommited();
        }
    }
}
