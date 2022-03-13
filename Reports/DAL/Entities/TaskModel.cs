using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Reports.Exceptions;

namespace Reports.DAL.Entities
{
    public class TaskModel
    {
        public TaskModel() { }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid Id { get; set; }
        public Guid AssignedEmployeeId { get; set; }
        public TaskCondition Condition { get; set; }
    }
}