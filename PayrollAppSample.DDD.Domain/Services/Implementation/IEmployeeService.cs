using System.Collections.Generic;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.ViewModels;

namespace PayrollAppSample.DDD.Domain.Services.Implementation {
    public interface IEmployeeService {
        Employee AddEmployee(EmployeeViewModel model);
        Employee ApplySalaryIncreaseCustom(int employeeId, decimal customIncreaseSalaryAmt);
        decimal ApplySalaryIncreasePerPositionPercentage(int employeeId);
        List<Employee> ApplySalaryIncreaseToSpecificDepartment(int deptId, int increasePercentage);
        void AssignDepartment(int employeeId, int deptId);
        void DeleteEmployee(int empId);
        void DeleteMultipleEmployees(int[] empIds);
        List<Timekeeping> GetListOfEmployeeTimekeeping();
        decimal GetNetPayWithTaxDeductionOnly(int empId);
        decimal GetYearlyIncome();
        Employee InitializeEmployeeSalaryUsingPosition(int employeeId);
    }
}