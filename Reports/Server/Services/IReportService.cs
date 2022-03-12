using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        Task<Report> AppointReportAsync(string deadLineTime, Guid assignedEmployeeId);
        IEnumerable<Report> GetByEmployeeId(Guid employeeId);
        Task FillReportAsync(Guid reportId, string text);
        IEnumerable<Report> GetReadyReportsOfSubordinates(Guid employeeId);
        IEnumerable<Report> GetUnfinishedReportsOfSubordinates(Guid employeeId);
    }
}