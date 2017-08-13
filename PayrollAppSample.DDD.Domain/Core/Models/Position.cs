using PayrollAppSample.DDD.Domain.Core.Interfaces;

namespace PayrollAppSample.DDD.Domain.Core.Models {
    public class Position : IEntity {

        //public int Id { get; set; }
        public string Name { get; set; }
        public decimal BaseSalary { get; set; }
        public int IncreasePercentage { get; set; }
    }
}