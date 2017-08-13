using PayrollAppSample.DDD.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Persistence {
    public class UnitOfWork : IUnitOfWork {


        private readonly DbContext _context;
        public UnitOfWork(DbContext context) {
            _context = context;
        }

        public void Complete() {
            _context.SaveChanges();
        }

        public async Task CompleteAsync() {
            await _context.SaveChangesAsync();
        }
    }
}
