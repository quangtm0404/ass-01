using System.Linq.Expressions;

namespace Services.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void Insert(TEntity entity);
        bool SaveChanges();
        public List<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
    }
}
