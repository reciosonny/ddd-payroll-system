using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollAppSample.DDD.Domain.Core.Interfaces;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.Repository.Implementation;
using System.Linq.Expressions;

namespace PayrollAppSample.DDD.Domain.Services.Implementation {
    public class TimekeepingService {
        private IGenericRepository<Employee> _employeeRepository;
        private IGenericRepository<Timekeeping> _timekeepingRepository;
        private IUnitOfWork _uow;


        #region Stubs / Seams for Unit Testing
        public List<Timekeeping> stubTimekeepingLists = new List<Timekeeping>();
        #endregion

        public TimekeepingService(IGenericRepository<Employee> empStub, IGenericRepository<Timekeeping> timeStub, IUnitOfWork uowStub) {
            this._employeeRepository = empStub;
            this._timekeepingRepository = timeStub;
            this._uow = uowStub;
        }
        
        public Employee AwardLeaveCreditsToEmployee(int empId) {

            var model = _employeeRepository.FindItem(empId);
            var today = DateTime.Today;
            int numYears = today.Year - (model.DateHired.HasValue ? model.DateHired.Value.Year : 0);
            model.RemainingLeaveCredits = model.RemainingLeaveCredits ?? 0;

            for (int i = 0; i < numYears; i++) {
                var yearDeductor = numYears - i;
                var leaveCreditYear = today.Year - yearDeductor;

                Expression<Func<Timekeeping, bool>> query = x => x.LeaveCreditYear == leaveCreditYear;
                var modelTimekeeping = _timekeepingRepository.QueryItem(query);

                if (modelTimekeeping == null) {
                    stubTimekeepingLists.Add(new Timekeeping() {
                        Description = "Yearly leave credits grant",
                        EntryDate = new DateTime(today.Year - yearDeductor),
                        Remarks = "15 leave credits"
                    });
                    model.RemainingLeaveCredits += 15M;
                }
            }

            _uow.Complete();

            //model.RemainingLeaveCredits += numYears * 15;

            return model;
        }
    }
}
