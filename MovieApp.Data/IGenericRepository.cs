using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data
{

    public interface IGenericRepository<T> where T : class
    {
        ICollection<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        ICollection<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddAsync(T entity);
        void AddRange(ICollection<T> entities);
        void Delete(T entity);
        void DeleteRange(ICollection<T> entities);
        void Update(T entity);
        void UpdateRange(ICollection<T> entities);
        void Save();
    }
}
