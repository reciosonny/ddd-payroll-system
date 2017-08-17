using NSubstitute;
using NUnit.Framework;
using PayrollAppSample.DDD.Domain.Core.Interfaces;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.Repository.Implementation;
using PayrollAppSample.DDD.Domain.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Tests.Domain {
    [TestFixture]
    public class TestTimekeepingService {

        private IUnitOfWork uowStub;
        private IGenericRepository<EmployeeDeductions> empDeduStub;
        private IGenericRepository<Employee> empStub;
        private IGenericRepository<Timekeeping> timekeepingStub;
        private TimekeepingService timekeepingService = null;

        [SetUp]
        public void Init() {
            uowStub = Substitute.For<IUnitOfWork>();
            empDeduStub = Substitute
                .For<IGenericRepository<EmployeeDeductions>>();
            empStub = Substitute.For<IGenericRepository<Employee>>();
            timekeepingStub = Substitute
                .For<IGenericRepository<Timekeeping>>();
            timekeepingService = new TimekeepingService(empStub, timekeepingStub, uowStub);
        }

        
        [Test]
        public void AwardLeaveCreditsToEmployee_Should_AwardLeaveCredits() {
            var service = new TimekeepingService(empStub, timekeepingStub, uowStub);
            empStub.FindItem(Arg.Any<int>()).Returns(new Employee() {
                DateHired = new DateTime(2015, 8, 10)
            });
            Employee result = service.AwardLeaveCreditsToEmployee(Arg.Any<int>());


            Assert.AreEqual(30, result.RemainingLeaveCredits);
        }

        [Test]
        public void AwardLeaveCreditsToEmployee_WhenAwarded_AddsTimekeepingEntry() {
            var service = new TimekeepingService(empStub, timekeepingStub, uowStub);
            empStub.FindItem(Arg.Any<int>()).Returns(new Employee() {
                DateHired = new DateTime(2015, 8, 10)
            });
            service.AwardLeaveCreditsToEmployee(Arg.Any<int>());
            int result = service.stubTimekeepingLists.Count;
            //Employee result = service.AwardLeaveCreditsToEmployee(Arg.Any<int>());


            //Assert.AreEqual(30, result.RemainingLeaveCredits);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void AwardLeaveCreditsToEmployee_WhenAwardedPreviously_ShouldNotAwardLeaveCreditsOfThatYear() {
            empStub.FindItem(Arg.Any<int>()).Returns(new Employee() {
                DateHired = new DateTime(2015, 8, 10),
                RemainingLeaveCredits = 30M
            });
            timekeepingStub
                .QueryItem(Arg.Any<Expression<Func<Timekeeping, bool>>>())
                .Returns(new Timekeeping()); //we give QueryItem an instantiation to know that timekeeping is already awarded for 2 years.

            Employee result = timekeepingService.AwardLeaveCreditsToEmployee(Arg.Any<int>());
            //Employee result = service.AwardLeaveCreditsToEmployee(Arg.Any<int>());


            //Assert.AreEqual(30, result.RemainingLeaveCredits);
            Assert.AreEqual(30M, result.RemainingLeaveCredits);
        }
    }
}
