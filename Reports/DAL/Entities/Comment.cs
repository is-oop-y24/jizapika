using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Reports.DAL.Entities
{
    [Keyless]
    public class Comment
    {
        public Comment() { }
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid TaskId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}