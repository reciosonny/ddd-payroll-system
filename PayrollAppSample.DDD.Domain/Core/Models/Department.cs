using PayrollAppSample.DDD.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace PayrollAppSample.DDD.Domain.Core.Models {
    public class Department : IEntity {
        //public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}