using PayrollAppSample.DDD.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Core.Models {
    public class Employee : IEntity {

        //public int Id { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public int? DepartmentId { get; set; }
        public int? PositionId { get; set; }
        public decimal CurrentSalary { get; set; }
        public string EmploymentStatus { get; set; }
        public bool PayrollIncluded { get; set; }
        public int? PayrollHistoryId { get; set; }
        public int? TimekeepingId { get; set; }
        public DateTime? DateHired { get; set; }
        public int? EmployeeIncludedDeductionsId { get; set; }
        public decimal? RemainingLeaveCredits { get; set; }

        #region Navigation Properties
        public virtual Position Position { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual Department Department { get; set; }
        public virtual ICollection<PayrollHistory> PayrollHistories { get; set; }
        public virtual ICollection<Timekeeping> Timekeepings { get; set; }
        public EmployeeIncludedDeductions EmployeeIncludedDeductions { get; set; }

        #endregion



        public decimal GetEmployeeYearlyNetIncome() {
            return this.Position.BaseSalary * 12;
        }


    }
}
