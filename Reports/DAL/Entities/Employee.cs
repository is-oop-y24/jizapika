using System;

namespace Reports.DAL.Entities
{
    public class Employee
    {
        public Employee() { }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SupervisorId { get; set; }
    }
}