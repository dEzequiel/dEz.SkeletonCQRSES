using dEz.SkeletonCQRSES.Query.Domain.Entities;
using dEz.SkeletonCQRSES.Query.Domain.Repositories;
using dEz.SkeletonCQRSES.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace dEz.SkeletonCQRSES.Query.Infrastructure.Repositories
{
    public sealed class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        private readonly DatabaseContextFactory _contextFactory;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repositoryContext"></param>
        public CompanyRepository(DatabaseContext repositoryContext, DatabaseContextFactory contextFactory)
            : base(repositoryContext)
        {
            _contextFactory = contextFactory;
        }

        ///<inheritdoc cref="ICompanyRepository"/>
        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Companies.AsNoTracking().ToListAsync();
        }

        ///<inheritdoc cref="ICompanyRepository"/>
        public async Task<Company?> GetAsync(Guid id)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Companies
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        ///<inheritdoc cref="ICompanyRepository"/>
        public async Task AddAsync(Company company)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Companies.Add(company);
            await context.SaveChangesAsync();
        }

        ///<inheritdoc cref="ICompanyRepository"/>
        public async Task DeleteAsync(Company company)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Companies.Remove(company);
            await context.SaveChangesAsync();

        }

        ///<inheritdoc cref="ICompanyRepository"/>
        public async Task UpdateAsync(Company company)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Companies.Update(company);
            await context.SaveChangesAsync();

        }
    }
}
