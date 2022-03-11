using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public class TaskService : ITaskService
    {
        public List<TaskModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public TaskModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public TaskModel GetByCreationTime(string creationDate)
        {
            throw new NotImplementedException();
        }

        public TaskModel GetByLastChangeTime(string changeDate)
        {
            throw new NotImplementedException();
        }

        public List<TaskModel> GetAssignedByEmployeeId(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public List<TaskModel> GetChangedByEmployeeId(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskModel> CreateAsync(string name, string text, Guid assignedUserId)
        {
            throw new NotImplementedException();
        }

        public Task ChangeCondition(Guid id, TaskCondition condition)
        {
            throw new NotImplementedException();
        }

        public Task AddComment(Guid employeeId, Guid taskId, string message)
        {
            throw new NotImplementedException();
        }

        public Task ChangeAssignedEmployee(Guid newAssignedEmployeeId, Guid taskId)
        {
            throw new NotImplementedException();
        }

        public List<TaskModel> GetAllSubordinatesTasks(Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }
}