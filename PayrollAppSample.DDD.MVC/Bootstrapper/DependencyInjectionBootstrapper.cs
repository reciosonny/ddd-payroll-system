using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.Persistence;
using PayrollAppSample.DDD.Domain.Repository.Implementation;
using PayrollAppSample.DDD.Domain.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollAppSample.DDD.MVC.Bootstrapper {
    public static class DependencyInjectionBootstrapper {
        
        public static IHumanResourceService InjectEmployeeServiceDependencies() {
            IHumanResourceService _empService;
            var dbContext = new PayrollContext();
            var empRepository = new GenericRepository<Employee>(dbContext);
            var taxRepository = new GenericRepository<TaxTable>(dbContext);
            var deptRepository = new GenericRepository<Department>(dbContext);
            var uow = new UnitOfWork(dbContext);

            _empService = new HumanResourceService(empRepository, deptRepository, taxRepository, uow);

            return _empService;
        }
    }
}