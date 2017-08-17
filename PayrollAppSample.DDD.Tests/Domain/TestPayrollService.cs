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
    public class TestPayrollService {


        private IUnitOfWork uowStub;
        private IGenericRepository<EmployeeDeductions> empDeduStub;
        private IGenericRepository<Employee> empStub;
        [SetUp]
        public void Init() {
            uowStub = Substitute.For<IUnitOfWork>();
            empDeduStub = 
                Substitute
                .For<IGenericRepository<EmployeeDeductions>>();
            empStub = Substitute.For<IGenericRepository<Employee>>();

        }

        [Test]
        public void AddDeductionType_Should_AddADeduction() {
            
            var service = new PayrollService(empDeduStub, uowStub);
            EmployeeDeductions result = service.AddDeductionType(new EmployeeDeductions() {
                IsFixedAmt = true,
                FixedAmtDeduction = 2450M,
                Name = "SSS"
            });

            Assert.AreEqual("SSS", result.Name);
        }

        [Test]
        public void GetEmployeePayrollHistoryTotalNetpay_Should_GetTotal() {

            var service = new PayrollService(empStub, uowStub);
            var list = new List<PayrollHistory>();
            for (int i = 0; i < 4; i++) {
                list.Add(new PayrollHistory() {
                    GrossIncome = 25000M,
                    NetIncome = 15000M
                });
            }

            empStub.FindItem(Arg.Any<int>()).Returns(new Employee() {
                PayrollHistories = list
            });

            decimal result = service.GetEmployeePayrollHistoryTotalNetpay(Arg.Any<int>());

            Assert.AreEqual(60000M, result);

        }


        [Test]
        public void GetAllIncludedDeductions_Should_GetAllEmployeesDeductions() {

            var service = new PayrollService(empStub, uowStub);
            var listDeductions = new List<EmployeeDeductions>();

            listDeductions.Add(new EmployeeDeductions() {
                FixedAmtDeduction = 150M,
                Name = "SSS"
            });
            listDeductions.Add(new EmployeeDeductions() {
                FixedAmtDeduction = 500M,
                Name = "PagIbig"
            });
            listDeductions.Add(new EmployeeDeductions() {
                FixedAmtDeduction = 1000M,
                Name = "Philhealth"
            });

            empStub
                .FindItem(Arg.Any<int>())
                .Returns(new Employee() {
                    EmployeeIncludedDeductions = new EmployeeIncludedDeductions() {
                        EmployeeDeductions = listDeductions
                    }
                });

            decimal result = service.GetAllIncludedDeductions(Arg.Any<int>());

            Assert.AreEqual(1650M, result);
        }



    }
}
