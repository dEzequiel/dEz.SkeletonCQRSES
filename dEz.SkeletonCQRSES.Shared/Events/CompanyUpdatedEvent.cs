﻿using dEz.SkeletonCQRSES.ES.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Shared.Events
{
    /// <summary>
    /// CompanyUptadedEvent will be raised when the company updated command has been handled.
    /// </summary>
    public class CompanyUpdatedEvent : BaseEvent
    {
        public CompanyUpdatedEvent() : base(nameof(CompanyUpdatedEvent))
        {
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
