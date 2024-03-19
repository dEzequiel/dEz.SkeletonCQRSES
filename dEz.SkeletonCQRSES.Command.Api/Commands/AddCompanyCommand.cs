using dEz.SkeletonCQRSES.ES.Core.Commands;

namespace dEz.SkeletonCQRSES.Command.Api.Commands
{
    /// <summary>
    /// Represents the intention of creating a new company.
    /// </summary>
    public class AddCompanyCommand : BaseCommand
    {
        public string Name { get; set; }
        public string Addresss { get; set; }
        public string Country { get; set; }
    }
}
