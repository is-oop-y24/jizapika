using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Exceptions;
using Reports.DAL.Database;

namespace Reports.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReportsDatabaseContext _context;

        public EmployeeService(ReportsDatabaseContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToArrayAsync();
        }

        public async Task<Employee> CreateNewTeamLeadAsync(string name)
        {
            Employee employee = new ();
            employee.Name = name;
            employee.SupervisorId = Guid.Empty;
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> CreateAsync(string name, Guid supervisorId)
        {
            Employee employee = new ();
            employee.Name = name;
            employee.SupervisorId = supervisorId;
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (!employee.Id.Equals(id)) throw new ReportsException("Not found employee.");
            return employee;
        }

        public async Task<Employee> GetByNameAsync(string name)
        {
            Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.Name.Equals(name));
            if (!employee.Name.Equals(name)) throw new ReportsException("Not found employee.");
            return employee;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            Employee employee = await GetByIdAsync(id);
            if (SubordinatesByEmployeeId(id).ToArray().Length != 0)
                throw new ReportsException("This employee can't be deleted.");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByNameAsync(string name)
        {
            Employee employee = await GetByNameAsync(name);
            if (SubordinatesByEmployeeId(employee.Id).ToArray().Length != 0)
                throw new ReportsException("This employee can't be deleted.");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> SubordinatesByEmployeeId(Guid id)
        {
            return _context.Employees.Where(employee => employee.SupervisorId.Equals(id)).ToList();
        }

        public async Task UpdateSupervisorAsync(Guid updatingEmployeeId, Guid newSupervisorId)
        {
            Employee updatingEmployee = await GetByIdAsync(updatingEmployeeId);
            Employee newSupervisor = await GetByIdAsync(newSupervisorId);
            if (IsCurrentSubordinate(updatingEmployee, newSupervisor) ||
                newSupervisorId.Equals(updatingEmployeeId)) throw new ReportsException("Can't update supervisor.");
            updatingEmployee.SupervisorId = newSupervisorId;
            await _context.SaveChangesAsync();
        }

        private bool IsCurrentSubordinate(Employee maybeSupervisor, Employee current)
        {
            return SubordinatesByEmployeeId(maybeSupervisor.Id).Any(subordinate =>
                current.Id.Equals(subordinate.Id) || IsCurrentSubordinate(subordinate, current));
        }
    }
}