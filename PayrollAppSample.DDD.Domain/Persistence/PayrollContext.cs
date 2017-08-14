using PayrollAppSample.DDD.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Persistence {
    public class PayrollContext : DbContext {

        public PayrollContext() : base("Name=PayrollEntities") {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<TaxTable> TaxTable { get; set; }
        public DbSet<EmployeeIncludedDeductions> EmployeeIncludedDeductions { get; set; }
        public DbSet<PayrollEntry> PayrollEntries { get; set; }


    }
}
