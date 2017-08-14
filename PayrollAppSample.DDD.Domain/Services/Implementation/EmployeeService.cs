using AutoMapper;
using PayrollAppSample.DDD.Domain.Core.Interfaces;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.Persistence;
using PayrollAppSample.DDD.Domain.Repository.Implementation;
using PayrollAppSample.DDD.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Services.Implementation {
    public class EmployeeService : IEmployeeService {

        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IGenericRepository<Department> _deptRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TaxTable> _taxTableRepository;

        public EmployeeService(IGenericRepository<Employee> employeeRepository, IGenericRepository<Department> deptRepository, IGenericRepository<TaxTable> taxTableRepository, IUnitOfWork uow) {
            _deptRepository = deptRepository;
            _employeeRepository = employeeRepository; //new GenericRepository<Employee>(context);
            _taxTableRepository = taxTableRepository;
            _uow = uow; //new UnitOfWork(context);
        }

        #region For Unit-Testing Constructors
        public EmployeeService(IGenericRepository<Employee> employeeRepository, IUnitOfWork uow) {
            //var context = new PayrollContext();
            _employeeRepository = employeeRepository; //new GenericRepository<Employee>(context);
            //_mapper = mapper;
            _uow = uow; //new UnitOfWork(context);
        }

        public EmployeeService(IGenericRepository<Employee> employeeRepository, IGenericRepository<Department> deptRepository, IUnitOfWork uow) {
            //var context = new PayrollContext();
            _employeeRepository = employeeRepository;
            _deptRepository = deptRepository;
            _uow = uow;
        }
        public EmployeeService(IGenericRepository<Employee> employeeRepository, IGenericRepository<TaxTable> taxTableRepository, IUnitOfWork uow) {
            //var context = new PayrollContext();
            _employeeRepository = employeeRepository;
            _taxTableRepository = taxTableRepository;
        }

        #endregion

        public Employee AddEmployee(EmployeeViewModel model) {
            var data = new Employee() {
                Fname = model.Fname,
                Lname = model.Lname
            };
            _employeeRepository.Add(data);
            _uow.Complete();

            return data;
        }

        public void DeleteMultipleEmployees(int[] empIds) {
            var employees = new List<Employee>();

            foreach (var id in empIds) {
                var model = _employeeRepository.FindItem(id);
                employees.Add(model);
            }

            _employeeRepository.DeleteRange(employees);
            _uow.Complete();
        }

        public void DeleteEmployee(int empId) {
            var data = _employeeRepository.FindItem(empId);
            _employeeRepository.Delete(data);

            _uow.Complete();
        }

        public decimal GetYearlyIncome() {
            return _employeeRepository.GetFirstItem().GetEmployeeYearlyNetIncome();
        }

        public List<Timekeeping> GetListOfEmployeeTimekeeping() {
            return _employeeRepository.GetFirstItem().Timekeepings.ToList();
        }

        /// <summary>
        /// Applies an increase to all employees in that department depending on the percentage
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="increasePercentage"></param>
        /// <returns></returns>
        public List<Employee> ApplySalaryIncreaseToSpecificDepartment(int deptId, int increasePercentage) {
            var model = _deptRepository.FindItem(deptId);
            foreach (var item in model.Employees) {
                item.CurrentSalary += item.CurrentSalary * (increasePercentage * 0.01M);
                _employeeRepository.Update(item);
            }

            _uow.Complete();

            return model.Employees.ToList();
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

        public Employee InitializeEmployeeSalaryUsingPosition(int employeeId) {
            var model = _employeeRepository.FindItem(employeeId);
            model.CurrentSalary = model.Position.BaseSalary;
            _employeeRepository.Update(model);

            _uow.Complete();

            return model;
        }

        public decimal GetNetPayWithTaxDeductionOnly(int empId) {
            //Expression<Func<TaxTable, bool>> filterTaxTable = (x) => x.MaxSalary 

            var model = _employeeRepository.FindItem(empId);
            TaxTable taxTable = _taxTableRepository.QueryItem(x => x.MaxSalary >= model.CurrentSalary && x.MinSalary <= model.CurrentSalary);

            decimal result = model.CurrentSalary - (model.CurrentSalary * (taxTable.DeductionPercentage * 0.01M));

            return result;

        }
    }
}
