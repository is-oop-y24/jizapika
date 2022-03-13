using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> CreateNewTeamLeadAsync(string name);
        Task<Employee> CreateAsync(string name, Guid supervisorId);

        Task<Employee> GetByIdAsync(Guid id);
        Task<Employee> GetByNameAsync(string name);
        Task DeleteByIdAsync(Guid id);
        Task DeleteByNameAsync(string name);
        Task UpdateSupervisorAsync(Guid updatingEmployeeId, Guid newSupervisorId);
    }
}