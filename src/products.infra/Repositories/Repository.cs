using Microsoft.EntityFrameworkCore;
using products.domain.Repository;
using products.infra.Data;

namespace products.infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity obj)
        {
            _context.Add(obj);
            SaveChanges();
        }

        public IQueryable<TEntity> GetAll() => _dbSet.AsNoTracking();

        public TEntity GetById(string id) => _dbSet.Find(id);

        public void Remove(string id)
        {
            _context.Remove(id);
            SaveChanges();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _context.Update(obj);
            SaveChanges();
        }
    }
}
