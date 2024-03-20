using dEz.SkeletonCQRSES.ES.Core.Commands;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using dEz.SkeletonCQRSES.ES.Core.Queries;
using dEz.SkeletonCQRSES.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace dEz.SkeletonCQRSES.Query.Infrastructure.Dispatchers
{
    /// <summary>
    /// Implementation for dispatching queries.
    /// </summary>
    public class QueryDispatcher : IQueryDispatcher<Company>
    {
        private readonly Dictionary<Type, Func<BaseQuery, Task<IEnumerable<Company>>>> _handlers = new();


        /// <inheritdoc cref="IQueryDispatcher{TEntity}"/>
        public void RegisterHandler<TQuery>(Func<TQuery, Task<IEnumerable<Company>>> handler) where TQuery : BaseQuery
        {
            if (_handlers.ContainsKey(typeof(TQuery)))
            {
                throw new IndexOutOfRangeException("You cannot register the same command handler twice");
            }

            _handlers.Add(typeof(TQuery), x => handler((TQuery)x));
        }

        /// <inheritdoc cref="IQueryDispatcher{TEntity}"/>
        public async Task<IEnumerable<Company>> SendAsync(BaseQuery query)
        {
            if (_handlers.TryGetValue(query.GetType(), out Func<BaseQuery, Task<IEnumerable<Company>>> handler))
            {
                return await handler(query);
            }
            else
            {
                throw new ArgumentNullException(nameof(handler), "No query handler was registered");
            }
        }
    }
}
