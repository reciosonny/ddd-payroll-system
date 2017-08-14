using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using PayrollAppSample.DDD.Domain.Core.Interfaces;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.Repository.Implementation;
using PayrollAppSample.DDD.Domain.Services.Implementation;
using PayrollAppSample.DDD.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public void ApplySalaryIncreaseToSpecificDepartment_Should_IncreaseAllEmployeesOfThatDepartment() {
            #region Arrange
            var uowStub = Substitute.For<IUnitOfWork>();
            var repositoryStub = Substitute.For<IGenericRepository<Employee>>();
            var deptStub = Substitute.For<IGenericRepository<Department>>();
            var mapperStub = Substitute.For<IMapper>();
            var empLists = new List<Employee>();
            for (int i = 0; i < 5; i++) {
                empLists.Add(new Employee() {
                    Fname = "Random Employee",
                    CurrentSalary = 18000M
                });
            }

            deptStub.FindItem(Arg.Any<int>()).Returns(new Department() {
                Employees = empLists
            });
            var service = new EmployeeService(repositoryStub, deptStub, uowStub);
            #endregion

            List<Employee> empList = service.ApplySalaryIncreaseToSpecificDepartment(Arg.Any<int>(), 25);


            Assert.AreEqual(22500M, empList[4].CurrentSalary);
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

        [Test]
        public void InitializeEmployeeSalaryUsingPosition_Should_AddBaseSalaryToEmployee() {
            var uowStub = Substitute.For<IUnitOfWork>();
            var repositoryStub = Substitute.For<IGenericRepository<Employee>>();
            repositoryStub.FindItem(Arg.Any<int>()).Returns(new Employee() {
                Position = new Position() {
                    Name = "Sr. Programmer",
                    BaseSalary = 35000M
                }
            });

            var service = new EmployeeService(repositoryStub, uowStub);

            Employee emp = service.InitializeEmployeeSalaryUsingPosition(Arg.Any<int>());
            decimal result = emp.CurrentSalary;


            Assert.AreEqual(35000M, result);
        }

        [Test]
        public void AddEmployee_Should_Add() {
            var uowStub = Substitute.For<IUnitOfWork>();
            var repositoryStub = Substitute.For<IGenericRepository<Employee>>();
            var deptStub = Substitute.For<IGenericRepository<Department>>();
            var mapperStub = Substitute.For<IMapper>();
            var empLists = new List<Employee>();

            var service = new EmployeeService(repositoryStub, deptStub, uowStub);
            //mapperStub.Map<EmployeeViewModel, Employee>(Arg.Any<EmployeeViewModel>()).Returns(new Employee() {
            //    Fname = "Sonny",
            //    Lname = "Recio"
            //});
            var result = service.AddEmployee(new EmployeeViewModel() {
                Fname = "Sonny",
                Lname = "Recio"
            });


            Assert.AreEqual("Recio", result.Lname);
        }

        [Test]
        public void GetNetPayWithTaxDeductionOnly_Should_Deduct() {
            var uowStub = Substitute.For<IUnitOfWork>();
            var repositoryStub = Substitute.For<IGenericRepository<Employee>>();
            var deptStub = Substitute.For<IGenericRepository<Department>>();
            var taxStub = Substitute.For<IGenericRepository<TaxTable>>();

            taxStub
                .QueryItem(Arg.Any<Expression<Func<TaxTable, bool>>>())
                .Returns(new TaxTable() { DeductionPercentage = 15, MaxSalary = 25000M, MinSalary = 18000M });
            repositoryStub.FindItem(Arg.Any<int>()).Returns(new Employee() {
                CurrentSalary = 24000M
            });


            var service = new EmployeeService(repositoryStub, taxStub, uowStub);


            decimal result = service.GetNetPayWithTaxDeductionOnly(Arg.Any<int>());


            Assert.AreEqual(20400M, result);
        }


    }
}
