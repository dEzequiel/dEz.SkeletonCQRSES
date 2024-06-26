﻿using dEz.SkeletonCQRSES.ES.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.ES.Core.Domain
{
    /// <summary>
    /// Aggregate can be viewed as the domain entity on the write or command side. Unlike domain entities, it contains
    /// behaviour and its structure is due to the fundamental difference on how the data is stored on write/event stores 
    /// databases.
    /// 
    /// Aggregate should allow to be able to use the events to recreate or replay the latest state of the Aggregate,
    /// without querying read database of related service for the latest state.
    /// </summary>
    public abstract class AggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        protected Guid _id;

        /// <summary>
        /// List of changes that take action on the aggregate.
        /// Events are used to make state canges to the aggregate. Those historic of events
        /// are going to be registered as changes.
        /// </summary>
        private readonly List<BaseEvent> _changes = new();

        /// <summary>
        /// Returns identifier.
        /// </summary>
        public Guid Id { get { return _id; } }

        /// <summary>
        /// Aggregate version.
        /// -1 means aggregate has not yet given an official version because version zero would actually 
        /// be the first version for the aggregate since it is zero based.
        /// </summary>
        public int Version { get; set; } = -1;

        /// <summary>
        /// Returns uncommited changes.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseEvent> GetUncommitedChanges()
        {
            return _changes;
        }

        /// <summary>
        /// Changes should be cleared after the uncommitted changes altered the state of the aggregate.
        /// </summary>
        public void MarkChangesAsCommited()
        {
            _changes.Clear();
        }

        /// <summary>
        /// Apply changes done by the events to the inner state of the aggregate and optionally add those events
        /// to the uncommited list of events.
        /// 
        /// Using "reflection" calls the apply method on the concrete aggregate when a event is raised.
        /// </summary>
        /// <param name="event"></param>
        /// <param name="isNew"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void ApplyChange(BaseEvent @event, bool isNew)
        {
            var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method), $"The Apply method was not found in the aggregate for {@event.GetType().Name}!");
            }

            method.Invoke(this, new object[] { @event });

            if (isNew)
            {
                _changes.Add(@event);
            }
        }

        /// <summary>
        /// Raise an specific event.
        /// 
        /// Once events are raised, ApplyChange method uses "reflection" to try and figure out which
        /// apply method on the concrete aggregate it should invoke.
        /// </summary>
        /// <param name="event"></param>
        protected void RaiseEvent(BaseEvent @event)
        {
            ApplyChange(@event, true);
        }

        /// <summary>
        /// Replay a list of events on the aggregate.
        /// </summary>
        /// <param name="events"></param>
        public void ReplayEvents(IEnumerable<BaseEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyChange(@event, false);
            }
        }
    }
}
