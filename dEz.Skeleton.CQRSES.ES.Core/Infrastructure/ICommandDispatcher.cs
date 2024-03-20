using dEz.SkeletonCQRSES.ES.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.ES.Core.Infrastructure
{
    public interface ICommandDispatcher
    {
        /// <summary>
        /// RegisterHandler allows to register a command handler method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand;

        /// <summary>
        /// SendAsync dispatch the command object to the registered command handler method.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task SendAsync(BaseCommand command);
    }
}
