using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Reports.DAL.Entities
{
    [Keyless]
    public class Comment
    { 
        public Comment(){}
        public Comment(Guid employeeId, Guid taskId, string message)
        {
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId), "Employee Id is invalid");
            }

            if (taskId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId), "Employee Id is invalid");
            }

            Date = DateTime.Now;
            EmployeeId = employeeId;
            TaskId = taskId;
            Message = message;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public Guid EmployeeId { get; }
        public Guid TaskId { get; }
        public DateTime Date { get; }
        public string Message { get; }
    }
}