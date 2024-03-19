using dEz.SkeletonCQRSES.ES.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Shared.Events
{
    /// <summary>
    /// CompanyRemovedEvent will be raised when the company removed command has been handled.
    /// </summary>
    public class CompanyRemovedEvent : BaseEvent
    {
        public CompanyRemovedEvent() : base(nameof(CompanyRemovedEvent))
        {
        }

        public Guid CompanyId { get; set; }
    }
}
