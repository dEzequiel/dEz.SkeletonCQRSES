using dEz.SkeletonCQRSES.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Query.Abstraction
{
    /// <summary>
    /// Abstraction throught company related event objects can be handled once they have been consumed from kafka.
    /// </summary>
    public interface IEventHandler
    {
        Task On(CompanyCreatedEvent @event);
    }
}
