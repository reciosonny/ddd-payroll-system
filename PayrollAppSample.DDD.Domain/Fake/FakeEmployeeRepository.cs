using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PayrollAppSample.DDD.Domain.Fake {

    public class FakeEmployeeRepository : IGenericRepository<Employee> {

        public void Add(Employee entity) {
            throw new NotImplementedException();
        }

        public void Delete(Employee model) {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<Employee> model) {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> FindAllItems(int id) {
            throw new NotImplementedException();
        }

        public Employee FindItem(int id) {
            //throw new NotImplementedException();
            return new Employee() {
                Fname = "Sonny",
                Lname = "Recio"
            };
        }

        public IEnumerable<Employee> GetAllItems() {
            throw new NotImplementedException();
        }

        public Employee GetFirstItem() {
            throw new NotImplementedException();
        }

        public Employee QueryItem(Expression<Func<Employee, bool>> query) {
            throw new NotImplementedException();
        }

        public void Update(Employee entity) {
            throw new NotImplementedException();
        }
    }
}
