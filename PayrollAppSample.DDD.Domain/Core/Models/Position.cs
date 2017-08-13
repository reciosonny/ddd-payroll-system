namespace PayrollAppSample.DDD.Domain.Core.Models {
    public class Position {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BaseSalary { get; set; }
        public int IncreasePercentage { get; set; }
    }
}