using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskService
    {
        List<TaskModel> GetAll();

        TaskModel GetById(Guid id);

        TaskModel GetByCreationTime(string creationDate);

        TaskModel GetByLastChangeTime(string changeDate);

        List<TaskModel> GetAssignedByEmployeeId(Guid employeeId);

        List<TaskModel> GetChangedByEmployeeId(Guid employeeId);
        
        Task<TaskModel> CreateAsync(string name, string text, Guid assignedUserId);
        
        Task ChangeCondition(Guid id, TaskCondition condition);
        
        Task AddComment(Guid employeeId, Guid taskId, string message);
        
        Task ChangeAssignedEmployee(Guid newAssignedEmployeeId, Guid taskId);

        List<TaskModel> GetAllSubordinatesTasks(Guid employeeId);
    }
}