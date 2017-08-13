using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Core.Interfaces {
    public interface IUnitOfWork {

        void Complete();
        Task CompleteAsync();
    }
}
