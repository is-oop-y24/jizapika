using System;

namespace Reports.DAL.Entities
{
    public class Report
    {
        public Report(string reportInfo, DateTime deadLineTime)
        {
            ReportInfo = reportInfo;
            DeadLineTime = deadLineTime;
        }

        public Report()
        {
        }

        public DateTime DeadLineTime { get; }
        public string ReportInfo { get; }
    }
}