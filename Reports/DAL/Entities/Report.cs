using System;

namespace Reports.DAL.Entities
{
    public class Report
    {
        public Report() { }
        public DateTime DeadLineTime { get; set; }
        public string ReportInfo { get; set; }
        public Guid AssignedEmployeeId { get; set; }
        public Guid Id { get; set; }
    }
}