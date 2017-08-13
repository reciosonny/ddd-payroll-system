using NSubstitute;
using NUnit.Framework;
using PayrollAppSample.DDD.Domain.Core.Interfaces;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.Repository.Implementation;
using PayrollAppSample.DDD.Domain.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Tests.Domain {
    [TestFixture]
    public class TestEmployeeService {

        [Test]
        public void ApplySalaryIncreasePerPositionPercentage_Should_IncreaseEmployeeSalary() {
            var uowStub = Substitute.For<IUnitOfWork>();
            var repositoryStub = Substitute.For<IGenericRepository<Employee>>();
            repositoryStub.FindItem(Arg.Any<int>()).Returns(new Employee() {
                CurrentSalary = 15000,
                Position = new Position() {
                    Name = "Jr. Programmer",
                    BaseSalary = 15000,
                    IncreasePercentage = 25
                }
            });

            var service = new EmployeeService(repositoryStub, uowStub);
            decimal result = service.ApplySalaryIncreasePerPositionPercentage(Arg.Any<int>());

            Assert.AreEqual(18750M, result);
        }

        [Test]
        public void ApplySalaryIncreaseToSpecificDepartment_Should_ApplyIncreaseInSpecifiedDepartment() {
            var uowStub = Substitute.For<IUnitOfWork>();
            var repositoryStub = Substitute.For<IGenericRepository<Employee>>();
            var deptStub = Substitute.For<IGenericRepository<Department>>();
            var service = new EmployeeService(repositoryStub, deptStub, uowStub);
            
            List<Employee> empList = service.ApplySalaryIncreaseToSpecificDepartment(Arg.Any<int>());

        }

        [Test]
        public void ApplySalaryIncreaseCustom_Should_IncreaseEmployeeSalary() {
            var uowStub = Substitute.For<IUnitOfWork>();
            var repositoryStub = Substitute.For<IGenericRepository<Employee>>();
            repositoryStub.FindItem(Arg.Any<int>()).Returns(new Employee() {
                CurrentSalary = 15000,
                Position = new Position() {
                    Name = "Jr. Programmer",
                    BaseSalary = 15000,
                    IncreasePercentage = 25
                }
            });

            var service = new EmployeeService(repositoryStub, uowStub);

            Employee emp = service.ApplySalaryIncreaseCustom(Arg.Any<int>(), 20500M);
            decimal result = emp.CurrentSalary;


            Assert.AreEqual(20500M, result);
        }

    }
}
