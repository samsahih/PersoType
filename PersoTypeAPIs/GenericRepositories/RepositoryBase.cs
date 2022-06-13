using Microsoft.EntityFrameworkCore;
using PersoTypeAPIs.Models;
using System.Linq.Expressions;

namespace PersoTypeAPIs.GenericRepositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected PersoTypeDbContext PersoTypeDbContext { get; set; }
        public RepositoryBase(PersoTypeDbContext PersoTypeDbContext)
        {
            this.PersoTypeDbContext = PersoTypeDbContext;
        }
        public IQueryable<T> FindAll()
        {
            return this.PersoTypeDbContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.PersoTypeDbContext.Set<T>()
                .Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            this.PersoTypeDbContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            this.PersoTypeDbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            this.PersoTypeDbContext.Set<T>().Remove(entity);
        }
    }
}
