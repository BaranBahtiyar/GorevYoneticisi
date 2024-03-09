using System.Linq.Expressions;

namespace GorevYoneticisi.Model
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);

        void Ekle(T entity);
        void Guncelle(T entity);
    }
}
