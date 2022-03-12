using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Reports.DAL.Database;
using Reports.DAL.Entities;
using Reports.Exceptions;

namespace Reports.Server.Services
{
    public class TaskService : ITaskService
    {
        private readonly ReportsDatabaseContext _context;

        public TaskService(ReportsDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskModel>> GetAllAsync()
        {
            return await _context.Tasks.ToArrayAsync();
        }

        public async Task<TaskModel> GetByIdAsync(Guid id)
        {
            TaskModel task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (!task.Id.Equals(id)) throw new ReportsException("Not found task.");
            return task;
        }

        public IEnumerable<TaskModel> GetByCreationTime(string creationDate)
        {
            if (!DateTime.TryParseExact(creationDate, "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                System.Globalization.DateTimeStyles.AdjustToUniversal,
                out DateTime dateTime)) throw new ReportsException("Not correct date-format.");
            return _context.Tasks.Where(task => task.CreationDate.Date.Equals(dateTime.Date)).ToList();
        }

        public IEnumerable<TaskModel> GetByLastChangeTime(string changeDate)
        {
            if (!DateTime.TryParseExact(changeDate, "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.AllowWhiteSpaces |
                System.Globalization.DateTimeStyles.AdjustToUniversal,
                out DateTime dateTime)) throw new ReportsException("Not correct date-format.");
            List<TaskModel> tasks = new();
            foreach (TaskModel task in _context.Tasks)
            {
                DateTime lastDate = task.CreationDate;
                foreach (Comment comment in _context.Comments.Where(
                    comment => comment.TaskId.Equals(task.Id) && lastDate < comment.Date))
                {
                    lastDate = comment.Date;
                }
                if (lastDate.Date.Equals(dateTime.Date)) tasks.Add(task);
            }

            return tasks;
        }

        public IEnumerable<TaskModel> GetAssignedByEmployeeId(Guid employeeId)
        {
            return _context.Tasks.Where(task => task.AssignedEmployeeId.Equals(employeeId));
        }

        public IEnumerable<TaskModel> GetChangedByEmployeeId(Guid employeeId)
            => _context.Tasks.Where(task => _context.Comments.Any(comment => comment.EmployeeId.Equals(employeeId))).ToList();

        public async Task<TaskModel> CreateAsync(string name, string text, Guid assignedUserId)
        {
            TaskModel task = new ();
            task.Name = name;
            task.Text = text;
            task.AssignedEmployeeId = assignedUserId;
            task.Condition = TaskCondition.Open;
            task.CreationDate = DateTime.Now;
            task.Id = Guid.NewGuid();
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task ChangeConditionAsync(Guid id, string condition)
        {
            TaskModel task = await GetByIdAsync(id);
            task.Condition = condition switch
            {
                "open" => TaskCondition.Open,
                "active" => TaskCondition.Active,
                "resolved" => TaskCondition.Resolved,
                _ => throw new ReportsException("Not correct condition.")
            };
            await _context.SaveChangesAsync();
        }

        public async Task AddCommentAsync(Guid employeeId, Guid taskId, string message)
        {
            await _context.Comments.AddAsync(new Comment
            {
                Date = DateTime.Now,
                Id = Guid.NewGuid(),
                Message = message,
                EmployeeId = employeeId,
                TaskId = taskId
            });
        }

        public async Task ChangeAssignedEmployeeAsync(Guid newAssignedEmployeeId, Guid taskId)
        {
            TaskModel task = await GetByIdAsync(taskId);
            if (newAssignedEmployeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(newAssignedEmployeeId), "Id is invalid");
            }

            task.AssignedEmployeeId = newAssignedEmployeeId;
            await _context.SaveChangesAsync();
        }

        public IEnumerable<TaskModel> GetAllSubordinatesTasks(Guid employeeId)
        {
            List<TaskModel> tasks = new ();
            foreach (Employee employee in _context.Employees.Where(x => x.SupervisorId.Equals(employeeId)))
            {
                tasks.AddRange(_context.Tasks.Where(task => task.AssignedEmployeeId.Equals(employee.Id)));
            }

            return tasks;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            TaskModel task = await GetByIdAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}