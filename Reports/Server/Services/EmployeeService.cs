using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToArray();
        }

        public async Task<Employee> CreateNewTeamLeadAsync(string name)
        {
            Employee employee = new (name, Guid.Empty);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> CreateAsync(string name, Guid supervisorId)
        {
            Employee employee = new (name, supervisorId);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }


        public async Task DeleteAsync(Guid id)
        {
            Employee employee = await GetByIdAsync(id);
            if (SubordinatesByEmployeeId(id).ToArray().Length != 0)
                throw new ReportsException("This employee can't be deleted.");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> SubordinatesByEmployeeId(Guid id)
        {
            return _context.Employees.Where(employee => employee.SupervisorId.Equals(id)).ToList();
        }

       /* public Employee Update(Employee entity)
        {
        }*/
    }
}