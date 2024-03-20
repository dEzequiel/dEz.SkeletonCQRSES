using dEz.SkeletonCQRSES.Command.Domain.Aggregates;
using dEz.SkeletonCQRSES.ES.Core.Handlers;

namespace dEz.SkeletonCQRSES.Command.Api.Commands
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IEventSourcingHandler<CompanyAggregate> _eventSourcingHandler;

        public CommandHandler(IEventSourcingHandler<CompanyAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        /// <inheritdoc cref="ICommandHandler"/>
        public async Task HandleAsync(AddCompanyCommand command)
        {
            var aggregate = new CompanyAggregate(command.Id, command.Name, command.Addresss, command.Country);
            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        /// <inheritdoc cref="ICommandHandler"/>
        public async Task HandleAsync(DeleteCompanyCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.DeleteCompany(command.Id);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        /// <inheritdoc cref="ICommandHandler"/>
        public async Task HandleAsync(UpdateCompanyCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.UpdateCompany(command.Id, command.Name, command.Country, command.Address);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }
    }
}
