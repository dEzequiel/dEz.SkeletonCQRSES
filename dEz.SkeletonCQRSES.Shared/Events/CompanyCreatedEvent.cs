using dEz.SkeletonCQRSES.ES.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Shared.Events
{
    /// <summary>
    /// CompanyCreatedEvent will be raised when the company created command has been handled.
    /// </summary>
    public class CompanyCreatedEvent : BaseEvent
    {
        public CompanyCreatedEvent() : base(nameof(CompanyCreatedEvent))
        {
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
