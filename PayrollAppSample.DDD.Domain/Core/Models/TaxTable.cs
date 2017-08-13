using PayrollAppSample.DDD.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Core.Models {
    public class TaxTable : IEntity {
        public decimal MaxSalary { get; set; }
        public decimal MinSalary { get; set; }
        public int DeductionPercentage { get; set; }
    }
}
