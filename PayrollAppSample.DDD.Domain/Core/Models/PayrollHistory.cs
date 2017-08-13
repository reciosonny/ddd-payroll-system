using PayrollAppSample.DDD.Domain.Core.Interfaces;
using System;

namespace PayrollAppSample.DDD.Domain.Core.Models {
    public class PayrollHistory : IEntity {
        //public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal NetIncome { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal TotalDeductions { get; set; }
        public DateTime PayrollDate { get; set; }


        #region Navigation Properties
        public Employee Employee { get; set; }
        #endregion

    }
}