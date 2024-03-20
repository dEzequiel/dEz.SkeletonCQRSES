using dEz.SkeletonCQRSES.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Query.Domain.Repositories
{
    /// <summary>
    /// Repository interface for company-related operations.
    /// </summary>
    public interface ICompanyRepository
    {
        /// <summary>
        /// Asynchronously retrieves all companies.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning an enumerable collection of
        /// <see cref="Company"/>.</returns>
        Task<IEnumerable<Company>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves a company by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the company to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation, returning the <see cref="Company"/> object
        /// with the specified ID, or null if the company is not found.
        /// </returns>
        Task<Company?> GetAsync(Guid id);

        /// <summary>
        /// Create company.
        /// </summary>
        /// <param name="company">Company object to be created.</param>
        Task AddAsync(Company company);

        /// <summary>
        /// Delete company.
        /// </summary>
        Task DeleteAsync(Company company);
        
        /// <summary>
        /// Update company.
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        Task UpdateAsync(Company company);
    }
}
