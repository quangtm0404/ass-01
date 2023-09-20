using Microsoft.EntityFrameworkCore;
using Services.Repositories;
using System.Linq.Expressions;

namespace Repositories.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _dbContext;
        public bool SaveChanges() => _dbContext.SaveChanges() > 0;
        public GenericRepository(AppDbContext _context)
        {
            this._dbContext = _context;
            _dbSet = _context.Set<TEntity>();
        }


        private DbSet<TEntity> _dbSet;


        public void Delete(TEntity entity)
        {
            DetachedEntity();
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
                => includes.Aggregate(_dbSet.AsQueryable(),
                                            (entity, property) => entity.Include(property))
                                            .AsNoTracking().ToList();
        public void Insert(TEntity entity) => _dbSet.Add(entity);
        public void Update(TEntity entity)
        {
            //_dbContext.Set<TEntity>().Attach(entity);
            DetachedEntity();
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        private void DetachedEntity()
        {
            _dbContext.ChangeTracker.Clear();

        }
        public List<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
          => includes
         .Aggregate(_dbSet.AsQueryable(),
             (entity, property) => entity.Include(property))
         .Where(expression).AsNoTracking().ToList();

    }
}

