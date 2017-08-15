using AutoMapper;
using PayrollAppSample.DDD.Domain.Core.Interfaces;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Services.Implementation {

    public class PayrollService {

        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IGenericRepository<Department> _deptRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TaxTable> _taxTableRepository;
        private IGenericRepository<EmployeeDeductions> _employeeDeductionsRepository;
        
        public PayrollService(IGenericRepository<EmployeeDeductions> employeeDeductionsRepository, IUnitOfWork uow) {
            this._employeeDeductionsRepository = employeeDeductionsRepository;
            this._uow = uow;
        }

        public PayrollService(IGenericRepository<Employee> empStub, IUnitOfWork uowStub) {
            _employeeRepository = empStub;
            _uow = uowStub;
        }

        public EmployeeDeductions AddDeductionType(EmployeeDeductions model) {

            _employeeDeductionsRepository.Add(model);
            _uow.Complete();


            return model;
        }

        public decimal GetAllIncludedDeductions(int empId) {
            var model = _employeeRepository.FindItem(empId);

            return model
                .EmployeeIncludedDeductions
                .EmployeeDeductions
                .Sum(x => x.FixedAmtDeduction);
        }

        public decimal GetEmployeePayrollHistoryTotalNetpay(int empId) {
            var model = _employeeRepository.FindItem(empId);

            return model
                .PayrollHistories
                .Sum(x => x.NetIncome);
        }
    }
}
