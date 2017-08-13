using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PayrollAppSample.DDD.Domain.Repository.Implementation {
    public interface IGenericRepository<T> where T : class {
        void Add(T entity);
        void Delete(T model);
        void DeleteRange(IEnumerable<T> model);
        T GetFirstItem();
        void Update(T entity);
        T FindItem(int id);
        IEnumerable<T> FindAllItems(int id);
        IEnumerable<T> GetAllItems();
        T QueryItem(Expression<Func<T, bool>> query);
    }
}