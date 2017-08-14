using AutoMapper;
using NUnit.Framework;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.Persistence;
using PayrollAppSample.DDD.Domain.Repository.Implementation;
using PayrollAppSample.DDD.Domain.Services.Implementation;
using PayrollAppSample.DDD.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.IntegrationTests.Domain {
    [TestFixture]
    [Ignore("Prioritize Unit Tests first")]
    public class TestHumanResourceService_Integration {

        public TestHumanResourceService_Integration() {

        }



        private void InitializeObjects() {

        }


        [Test]
        public void AddEmployee_Should_AddToDb() {
            var dbContext = new PayrollContext();
            var empRepo = new GenericRepository<Employee>(dbContext);
            var uow = new UnitOfWork(dbContext);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeViewModel, Employee>());
            var mapper = new Mapper(config);

            var service = new HumanResourceService(empRepo, uow);

            var result = service.AddEmployee(new DDD.Domain.ViewModels.EmployeeViewModel() {
                Fname = "Sonny",
                Mname = "Ramirez",
                Lname = "Recio"
            });

            Assert.AreEqual("Recio", result.Lname);

            service.RemoveEmployee(result.Id);
        }


    }
}
