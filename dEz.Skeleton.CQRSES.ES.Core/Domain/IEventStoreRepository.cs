﻿using dEz.SkeletonCQRSES.ES.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.ES.Core.Domain
{
    public interface IEventStoreRepository
    {
        /// <summary>
        /// Store an event.
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task SaveAsync(EventModel @event);

        /// <summary>
        /// Asynchronously retrieves all events related to a aggregate.
        /// </summary>
        /// <param name="aggregateId">The unique identifier of the aggregate to retrieve events.</param>
        /// <returns>A task representing the asynchronous operation, returning an enumerable collection of
        /// <see cref="EventModel"/>.</returns>
        Task<IEnumerable<EventModel>> FindByAggregateId(Guid aggregateId);
        Task<IEnumerable<EventModel>> FindAllAsync();

    }
}
