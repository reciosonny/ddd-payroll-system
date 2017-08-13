using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.ViewModels {
    public class EmployeeViewModel {
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public decimal CurrentSalary { get; set; }
        public string EmploymentStatus { get; set; }
        public bool PayrollIncluded { get; set; }
    }
}
