using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Database;
using Reports.DAL.Entities;
using Reports.Exceptions;

namespace Reports.Server.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportsDatabaseContext _context;

        public ReportService(ReportsDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Report> AppointReportAsync(string deadLineTime, Guid assignedEmployeeId)
        {
            Report report = new ();
            if (!DateTime.TryParseExact(deadLineTime, "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                System.Globalization.DateTimeStyles.AdjustToUniversal,
                out DateTime dateDeadLineTime)) throw new ReportsException("Not correct date-format.");
            report.DeadLineTime = dateDeadLineTime;

            Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id.Equals(assignedEmployeeId));
            if (!employee.Id.Equals(assignedEmployeeId)) throw new ReportsException("Not found employee.");

            report.AssignedEmployeeId = assignedEmployeeId;
            report.ReportInfo = string.Empty;
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public IEnumerable<Report> GetByEmployeeId(Guid employeeId)
        {
            return _context.Reports.Where(report => report.AssignedEmployeeId.Equals(employeeId));
        }

        public async Task FillReportAsync(Guid reportId, string text)
        {
            if (text == string.Empty)
                throw new ReportsException("The text is empty");
            Report report = await _context.Reports.FirstOrDefaultAsync(report => report.Id.Equals(reportId));
            report.ReportInfo = text;
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Report> GetReadyReportsOfSubordinates(Guid employeeId)
        {
            IEnumerable<Employee> subordinates = _context.Employees.Where(employee =>
                employee.SupervisorId.Equals(employeeId)).ToList();
            return _context.Reports.Where(report => report.ReportInfo.Length != 0 && subordinates.Any(
                employee => employee.Id.Equals(report.AssignedEmployeeId)));
        }

        public IEnumerable<Report> GetUnfinishedReportsOfSubordinates(Guid employeeId)
        {
            IEnumerable<Employee> subordinates = _context.Employees.Where(employee =>
                employee.SupervisorId.Equals(employeeId)).ToList();
            return _context.Reports.Where(report => report.ReportInfo.Length == 0 && subordinates.Any(
                employee => employee.Id.Equals(report.AssignedEmployeeId)));
        }
    }
}