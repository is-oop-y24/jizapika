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
        private List<Comment> _comments;

        public TaskModel(string name, string text, Guid assignedEmployeeId)
        {
            if (assignedEmployeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(assignedEmployeeId), "Id is invalid");
            }

            Name = name ?? throw new ArgumentNullException(nameof(name), "Name is invalid");
            Text = text ?? throw new ArgumentNullException(nameof(text), "Text is invalid");
            AssignedEmployeeId = assignedEmployeeId;
            Id = Guid.NewGuid();
            Condition = TaskCondition.Open;
            _comments = new List<Comment>();
            CreationDate = DateTime.Now;
        }

        public TaskModel()
        {
        }

        public string Name { get; }
        public string Text { get; }
        public DateTime CreationDate { get; }

        [Key]
        public Guid Id { get; }
        public Guid AssignedEmployeeId { get; private set; }
        public TaskCondition Condition { get; }

        [NotMapped]
        public ImmutableList<Comment> ImmutableComments => _comments.ToImmutableList();

        public void AddComment(string message, Guid employeeId)
        {
            _comments.Add(new Comment(employeeId, Id, message));
        }

        public void ChangeAssignedEmployee(Guid newAssignedEmployeeId)
        {
            if (newAssignedEmployeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(newAssignedEmployeeId), "Id is invalid");
            }

            AssignedEmployeeId = newAssignedEmployeeId;
        }

        public bool IsCreationInThisDate(string date)
        {
            if (!DateTime.TryParseExact(date, "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                System.Globalization.DateTimeStyles.AdjustToUniversal,
                out DateTime dateTime)) throw new ReportsException("Not correct date-format.");
            return (CreationDate.Date.Equals(dateTime.Date));
        }

        public bool IsLastEditionInThisDate(string date)
        {
            if (!DateTime.TryParseExact(date, "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                System.Globalization.DateTimeStyles.AdjustToUniversal,
                out DateTime dateTime)) throw new ReportsException("Not correct date-format.");
            return (LastEditionDate().Date.Equals(dateTime.Date));
        }

        private DateTime LastEditionDate()
        {
            DateTime lastDate = CreationDate;
            foreach (Comment comment in _comments.Where(comment => lastDate < comment.Date))
            {
                lastDate = comment.Date;
            }

            return lastDate;
        }
    }
}