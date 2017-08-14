using PayrollAppSample.DDD.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Core.Models {
    public class EmployeeIncludedDeductions : IEntity {

        public int EmployeeDeductionsId { get; set; }
        public int EmployeeId { get; set; }

        public ICollection<EmployeeDeductions> EmployeeDeductions { get; set; }
        public Employee Employee { get; set; }


    }
}
