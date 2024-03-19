namespace dEz.SkeletonCQRSES.Command.Api.Commands
{
    /// <summary>
    /// Definition of command handlers.
    /// </summary>
    public interface ICommandHandler
    {
        Task HandleAsync(AddCompanyCommand command);
        Task HandleAsync(DeleteCompanyCommand command);
        Task HandleAsync(UpdateCompanyCommand command);
    }
}
