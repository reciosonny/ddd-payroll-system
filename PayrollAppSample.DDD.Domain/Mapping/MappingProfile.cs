using AutoMapper;
using PayrollAppSample.DDD.Domain.Core.Models;
using PayrollAppSample.DDD.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollAppSample.DDD.Domain.Mapping {
    class MappingProfile : Profile {

        public MappingProfile() {
            CreateMap<Employee, EmployeeViewModel>();


            CreateMap<EmployeeViewModel, Employee>();

        }
    }
}
