using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        Task<Employee> CreateNewTeamLeadAsync(string name);
        Task<Employee> CreateAsync(string name, Guid supervisorId);

        Task<Employee> GetByIdAsync(Guid id);

        Task DeleteAsync(Guid id);

       // Employee Update(Employee entity);
    }
}