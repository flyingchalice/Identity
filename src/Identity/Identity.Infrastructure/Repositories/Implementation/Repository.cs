using Identity.Domain.Exceptions;
using System.Linq.Expressions;

namespace Identity.Infrastructure.Repositories.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly IdentityContext _context;

        public Repository(IdentityContext context)
        {
            _context = context;
        }

        public TEntity Get(int id)
        {
            var entity = _context.Find<TEntity>(id);

            if (entity == null)
            {
                throw new NotFoundException($"{EntityName} not found");
            }

            return entity;
        }

        public TEntity Get(string id)
        {
            var entity = _context.Find<TEntity>(id);

            if (entity == null)
            {
                throw new NotFoundException($"{EntityName} not found");
            }

            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public TEntity FindSingle(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return GetAll().Single(predicate);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException($"{EntityName} not found.");
            }
        }

        public IQueryable<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public TEntity Add(TEntity entity)
        {
            return _context.Add(entity).Entity;
        }

        public TEntity Update(TEntity entity)
        {
            return _context.Update(entity).Entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        private static string EntityName => typeof(TEntity).Name;
    }
}
