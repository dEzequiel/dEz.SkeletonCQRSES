using dEz.SkeletonCQRSES.ES.Core.Commands;

namespace dEz.SkeletonCQRSES.Command.Api.Commands
{
    /// <summary>
    /// Represents the intention of updating a company.
    /// </summary>
    public class UpdateCompanyCommand : BaseCommand
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
