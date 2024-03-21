using dEz.SkeletonCQRSES.Query.Domain.Entities;

namespace dEz.Skeleton.CQRSES.Query.Api.Queries
{
    /// <summary>
    /// Interface for handling queries.
    /// </summary>
    public interface IQueryHandler
    {
        /// <summary>
        /// Handler for retrieving company by id.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<Company>> HandleAsync(FindCompanyByIdQuery query);
        
        /// <summary>
        /// Handler for retrieving all companies.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<Company>> HandleAsync(FindAllCompaniesQuery query);
    }
}
