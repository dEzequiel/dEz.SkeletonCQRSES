namespace dEz.SkeletonCQRSES.Query.Domain.Entities
{
    /// <summary>
    /// Company business entity.
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Company identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Company name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Company address.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Company country.
        /// </summary>
        public string? Country { get; set; }
    }
}
