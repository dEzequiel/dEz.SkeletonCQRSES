using dEz.SkeletonCQRSES.Query.Domain.Entities;
using dEz.SkeletonCQRSES.Query.Domain.Services;

namespace dEz.Skeleton.CQRSES.Query.Api.Queries
{
    /// <summary>
    /// Implementation for handling queries.
    /// </summary>
    public class QueryHandler : IQueryHandler
    {
        private readonly ICompanyService _companyService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public QueryHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <inheritdoc cref="IQueryHandler"/>
        public async Task<IEnumerable<Company>> HandleAsync(FindCompanyByIdQuery query)
        {
            var company = await _companyService.GetByIdAsync(query.Id);
            return new List<Company>() { company };
        }

        /// <inheritdoc cref="IQueryHandler"/>
        public async Task<IEnumerable<Company>> HandleAsync(FindAllCompaniesQuery query)
        {
            var companies = await _companyService.GetAllAsync();
            return companies;
        }
    }
}
