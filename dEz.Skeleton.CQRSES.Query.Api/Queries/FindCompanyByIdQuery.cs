using dEz.SkeletonCQRSES.ES.Core.Queries;

namespace dEz.Skeleton.CQRSES.Query.Api.Queries
{
    /// <summary>
    /// Represents the intention of getting company by its identifier.
    /// </summary>
    public class FindCompanyByIdQuery : BaseQuery
    {
        public Guid Id { get; set; }
    }
}
