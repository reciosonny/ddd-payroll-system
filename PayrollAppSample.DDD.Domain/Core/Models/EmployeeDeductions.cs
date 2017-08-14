using PayrollAppSample.DDD.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Core.Models {
    public class EmployeeDeductions : IEntity {

        public string Name { get; set; }
        public decimal FixedAmtDeduction { get; set; }
        public decimal PercentageAmtDeduction { get; set; }
        public bool IsFixedAmt { get; set; }

    }
}
