using dEz.SkeletonCQRSES.Query.Abstraction;
using dEz.SkeletonCQRSES.Query.Domain.Services;
using dEz.SkeletonCQRSES.Shared.DTOs;
using dEz.SkeletonCQRSES.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Query.Infrastructure.Handlers
{
    public class EventHandler : IEventHandler
    {
        private readonly ICompanyService _service;
        public EventHandler(ICompanyService service)
        {
            _service = service;
        }

        public async Task On(CompanyCreatedEvent @event)
        {
            var company = new CompanyForAdd(@event.Id, @event.Name, @event.Address, @event.Country);
            await _service.AddAsync(company);
        }
    }
}
