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





    }
}
