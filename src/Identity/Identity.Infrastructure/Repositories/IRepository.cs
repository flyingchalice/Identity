using System.Linq.Expressions;

namespace Identity.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        TEntity Get(int id);
        TEntity Get(string id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate);
        TEntity FindSingle(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
