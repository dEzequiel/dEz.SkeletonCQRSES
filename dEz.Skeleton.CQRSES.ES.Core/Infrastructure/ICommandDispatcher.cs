using dEz.SkeletonCQRSES.ES.Core.Commands;

namespace dEz.SkeletonCQRSES.ES.Core.Infrastructure
{
    /// <summary>
    /// Interface for dispatching commands.
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Registers a handler for a specific command.
        /// </summary>
        /// <typeparam name="TCommand">The type of command to handle.</typeparam>
        /// <param name="handler">The handler function for the command.</param>
        void RegisterHandler<TCommand>(Func<TCommand, Task> handler) where TCommand : BaseCommand;

        /// <summary>
        /// Asynchronously dispatch a command.
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendAsync(BaseCommand command);
    }
}
