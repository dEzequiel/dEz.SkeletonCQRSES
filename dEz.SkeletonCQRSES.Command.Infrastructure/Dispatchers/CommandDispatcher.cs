
using dEz.SkeletonCQRSES.ES.Core.Commands;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Command.Infrastructure.Dispatchers
{
    /// <summary>
    /// Implementation for dispatching commands.
    /// </summary>
    public class CommandDispatcher : ICommandDispatcher
    {
        /// <summary>
        /// Registered handlers.
        /// </summary>
        private readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new();

        /// <inheritdoc cref="ICommandDispatcher"/>
        public void RegisterHandler<TCommand>(Func<TCommand, Task> handler) where TCommand : BaseCommand
        {
            if (_handlers.ContainsKey(typeof(TCommand)))
            {
                throw new IndexOutOfRangeException("You cannot register the same query handler twice");
            }

            _handlers.Add(typeof(TCommand), x => handler((TCommand)x));
        }

        /// <inheritdoc cref="ICommandDispatcher"/>
        /// <summary>From the registered handlers determining its type calls handler function for this type.</summary>
        public async Task SendAsync(BaseCommand command)
        {
            if (_handlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task> handler))
            {
                await handler(command);
            }
            else
            {
                throw new ArgumentNullException(nameof(handler), "No command handler was registered");
            }
        }
    }
}
