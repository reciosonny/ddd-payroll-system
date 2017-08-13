using PayrollAppSample.DDD.Domain.Core.Interfaces;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.Persistence;
using PayrollAppSample.DDD.Domain.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Services.Implementation {
    public class EmployeeService {

        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IGenericRepository<Department> _deptRepository;
        private readonly IUnitOfWork _uow;

        public EmployeeService(IGenericRepository<Employee> employeeRepository, IGenericRepository<Department> deptRepository, IUnitOfWork uow) {
            //var context = new PayrollContext();

            _deptRepository = deptRepository;
            _employeeRepository = employeeRepository; //new GenericRepository<Employee>(context);
            _uow = uow; //new UnitOfWork(context);
        }

        public EmployeeService(IGenericRepository<Employee> employeeRepository, IUnitOfWork uow) {
            //var context = new PayrollContext();
            _employeeRepository = employeeRepository; //new GenericRepository<Employee>(context);
            _uow = uow; //new UnitOfWork(context);
        }

        public void AddEmployee(Employee model) {
            _employeeRepository.Add(model);
            _uow.Complete();
        }

        public decimal GetYearlyIncome() {
            return _employeeRepository.GetFirstItem().GetEmployeeYearlyNetIncome();
        }

        public List<Timekeeping> GetListOfEmployeeTimekeeping() {
            return _employeeRepository.GetFirstItem().Timekeepings.ToList();
        }

        public List<Employee> ApplySalaryIncreaseToSpecificDepartment(int v) {
            throw new NotImplementedException();
        }

        public decimal ApplySalaryIncreasePerPositionPercentage(int employeeId) {
            var model = _employeeRepository.FindItem(employeeId);
            Func<decimal, decimal, decimal> IncreaseFormula = (baseSalary, increasePercentage) => {
                return baseSalary + (baseSalary * (increasePercentage * 0.01M));
            };

            var result = IncreaseFormula(model.Position.BaseSalary, model.Position.IncreasePercentage);

            model.CurrentSalary = result;
            _uow.Complete();

            return result;
        }

        public Employee ApplySalaryIncreaseCustom(int employeeId, decimal customIncreaseSalaryAmt) {
            var model = _employeeRepository.FindItem(employeeId);

            model.CurrentSalary = customIncreaseSalaryAmt;
            _uow.Complete();

            return model;
        }

        public void AssignDepartment(int employeeId, int deptId) {
            var model = _employeeRepository.FindItem(employeeId);
            var deptModel = _deptRepository.FindItem(deptId);
            model.Department = deptModel;

            _employeeRepository.Update(model);
            _uow.Complete();
        }

    }
}
