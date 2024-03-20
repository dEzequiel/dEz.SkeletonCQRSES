using dEz.SkeletonCQRSES.Query.Domain.Entities;
using dEz.SkeletonCQRSES.Query.Domain.Repositories;
using dEz.SkeletonCQRSES.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<Company>> GetAllAsync(bool trackChanges)
        {
            var companies = await FindAll(trackChanges).ToListAsync();
            return companies;
        }

        ///<inheritdoc cref="ICompanyRepository"/>
        public async Task<IEnumerable<Company>> GetAllByIdAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(c => ids.Contains(c.Id), trackChanges).ToListAsync();

        ///<inheritdoc cref="ICompanyRepository"/>
        public async Task<Company?> GetAsync(Guid id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        ///<inheritdoc cref="ICompanyRepository"/>
        public async Task AddAsync(Company company)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Companies.Add(company);
           _ = await context.SaveChangesAsync();
            
        }

        ///<inheritdoc cref="ICompanyRepository"/>
        public void DeleteAsync(Company company) =>
            Delete(company);
    }
}
