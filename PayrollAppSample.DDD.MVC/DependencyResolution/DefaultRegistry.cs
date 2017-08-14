// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PayrollAppSample.DDD.MVC.DependencyResolution {
    using PayrollAppSample.DDD.Domain.Core.Interfaces;
    using PayrollAppSample.DDD.Domain.Core.Models;
    using PayrollAppSample.DDD.Domain.Persistence;
    using PayrollAppSample.DDD.Domain.Repository.Implementation;
    using PayrollAppSample.DDD.Domain.Services.Implementation;
    using StructureMap;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
	
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });


            //For<IExample>().Use<Example>();
            For<System.Data.Entity.DbContext>().Use<PayrollContext>();
            For<IGenericRepository<Employee>>().Use<GenericRepository<Employee>>();
            For<IGenericRepository<TaxTable>>().Use<GenericRepository<TaxTable>>();
            For<IGenericRepository<Department>>().Use<GenericRepository<Department>>();
            For<IUnitOfWork>().Use<UnitOfWork>();

            For<IEmployeeService>().Use<EmployeeService>();

        }

        #endregion
    }
}