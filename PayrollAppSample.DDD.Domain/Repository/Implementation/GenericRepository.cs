using PayrollAppSample.DDD.Domain.Core.Interfaces;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Repository.Implementation {
    public class GenericRepository<T> : IGenericRepository<T>
        where T: IEntity {

        private readonly DbContext _context;
        public GenericRepository(DbContext context) {
            _context = context;
        }

        public void Add(T entity) {
            _context.Set<T>().Add(entity);
        }

        //Refer to this for various update strategy: https://stackoverflow.com/questions/15336248/entity-framework-5-updating-a-record
        public void Update(T entity) {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T model) {
            _context.Set<T>().Remove(model);
        }

        public void DeleteRange(IEnumerable<T> model) {
            _context.Set<T>().RemoveRange(model);
        }

        public T GetFirstItem() {
            return _context.Set<T>().First();
            //throw new NotImplementedException();
        }

        public T FindItem(int id) {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> FindAllItems(int id) {
            return _context.Set<T>().Where(x => x.Id == id);
        }

        public T QueryItem(Expression<Func<T, bool>> query) {
            return _context.Set<T>().Where(query).First();
        }

        public IEnumerable<T> GetAllItems() {
            return _context.Set<T>().AsEnumerable();
        }

    }
}
