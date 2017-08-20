using System.Collections.Generic;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.ViewModels;

namespace PayrollAppSample.DDD.Domain.Services.Implementation {
    public interface IHumanResourceService {
        Employee AddEmployee(EmployeeViewModel model);
        Employee ApplySalaryIncreaseCustom(int employeeId, decimal customIncreaseSalaryAmt);
        decimal ApplySalaryIncreasePerPositionPercentage(int employeeId);
        List<Employee> ApplySalaryIncreaseToSpecificDepartment(int deptId, int increasePercentage);
        void AssignDepartment(int employeeId, int deptId);
        void DeleteEmployee(int empId);
        void DeleteMultipleEmployees(int[] empIds);
        decimal GetNetPayWithTaxDeductionOnly(int empId);
        decimal GetYearlyIncome(int empId);
        Employee InitializeEmployeeSalaryUsingPosition(int employeeId);
        Employee RemoveEmployee(int empId);
        IEnumerable<Employee> GetListOfEmployees();
    }
}