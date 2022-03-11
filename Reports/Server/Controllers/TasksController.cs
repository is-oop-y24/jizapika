using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Reports.DAL.Entities;
using Reports.DAL.Database;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [Route("/tasks/create")]
        [HttpPost]
        public async Task<TaskModel> CreateTaskAsync(
            [FromQuery] string name, [FromQuery] string text, [FromQuery] Guid assignedEmployeeId)
        {
            return await _service.CreateAsync(name, text, assignedEmployeeId);
        }
    }
}