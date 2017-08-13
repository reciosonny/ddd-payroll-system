using PayrollAppSample.DDD.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Core.Models {
    public class Timekeeping : IEntity {

        //public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Entry { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string Remarks { get; set; }
        public bool IsPresent { get; set; }
        public DateTime EntryDate { get; set; }


        public Employee Employee { get; set; }

    }
}
