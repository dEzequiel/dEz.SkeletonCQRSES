using dEz.SkeletonCQRSES.ES.Core.Queries;

namespace dEz.SkeletonCQRSES.ES.Core.Infrastructure
{
    /// <summary>
    /// Interface for dispatching queries.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity the queries operate on.</typeparam>
    public interface IQueryDispatcher<TEntity>
    {
        /// <summary>
        /// Registers a handler for a specific query.
        /// </summary>
        /// <typeparam name="TQuery">The type of query to handle.</typeparam>
        /// <param name="handler">The handler function for the query.</param>
        /// <returns></returns>
        void RegisterHandler<TQuery>(Func<TQuery, Task<IEnumerable<TEntity>>> handler) where TQuery : BaseQuery;

        /// <summary>
        /// Asynchronously dispatch a query.
        /// </summary>
        /// <param name="query">The query to send.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<IEnumerable<TEntity>> SendAsync(BaseQuery query);
    }
}
