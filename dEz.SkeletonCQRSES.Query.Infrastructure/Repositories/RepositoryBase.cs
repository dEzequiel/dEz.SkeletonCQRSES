using dEz.SkeletonCQRSES.Query.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.Query.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DatabaseContext DatabaseContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repositoryContext"></param>
        protected RepositoryBase(DatabaseContext repositoryContext)
            => DatabaseContext = repositoryContext;

        ///<inheritdoc cref="IRepositoryBase{T}"/>
        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
                DatabaseContext.Set<T>()
                    .AsNoTracking() :
                DatabaseContext.Set<T>();

        ///<inheritdoc cref="IRepositoryBase{T}"/>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges) =>
            !trackChanges ?
                DatabaseContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking() :
                DatabaseContext.Set<T>()
                    .Where(expression);

        ///<inheritdoc cref="IRepositoryBase{T}"/>
        public async Task Create(T entity)
        {
            DatabaseContext.Set<T>().Add(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        ///<inheritdoc cref="IRepositoryBase{T}"/>
        public void Update(T entity)
        {
            DatabaseContext.Set<T>().Update(entity);
        }

        ///<inheritdoc cref="IRepositoryBase{T}"/>
        public void Delete(T entity)
        {
            DatabaseContext.Set<T>().Remove(entity);
        }
    }
}
