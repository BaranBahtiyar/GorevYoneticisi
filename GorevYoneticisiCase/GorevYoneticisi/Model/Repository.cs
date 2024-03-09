using GorevYoneticisi.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GorevYoneticisi.Model
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DBContextGY _dbContext;
        internal DbSet<T> set;


        public Repository(DBContextGY dbContext)
        {
            _dbContext = dbContext;
            this.set = _dbContext.Set<T>();
        }

        public void Ekle(T entity)
        {
            set.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Guncelle(T entity)
        {
            set.Update(entity);
            _dbContext.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> sorgu = set;
            sorgu = sorgu.Where(filter);
            return sorgu.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> sorgu = set;
            return sorgu.ToList();
        }
    }
}
