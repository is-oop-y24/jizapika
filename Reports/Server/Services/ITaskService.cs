using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> GetAllAsync();
        
        Task<TaskModel> GetByIdAsync(Guid id);
        
        IEnumerable<TaskModel> GetByCreationTime(string creationDate);

        IEnumerable<TaskModel> GetByLastChangeTime(string changeDate);

        IEnumerable<TaskModel> GetAssignedByEmployeeId(Guid employeeId);

        IEnumerable<TaskModel> GetChangedByEmployeeId(Guid employeeId);
        
        Task<TaskModel> CreateAsync(string name, string text, Guid assignedUserId);
        
        Task ChangeConditionAsync(Guid id, string condition);
        
        Task AddCommentAsync(Guid employeeId, Guid taskId, string message);
        
        Task ChangeAssignedEmployeeAsync(Guid newAssignedEmployeeId, Guid taskId);

        IEnumerable<TaskModel> GetAllSubordinatesTasks(Guid employeeId);

        IEnumerable<Comment> GetAllComments(Guid taskId);

        Task DeleteByIdAsync(Guid id);
    }
}