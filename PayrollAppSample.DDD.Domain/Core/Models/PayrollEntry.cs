using PayrollAppSample.DDD.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Core.Models {
    public class PayrollEntry : IEntity {

        public DateTime PayrollDate { get; set; }
        public int EmployeeId { get; set; }
        public decimal GrossPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay { get; set; }
        

        public ICollection<Employee> Employees { get; set; }

    }
}
