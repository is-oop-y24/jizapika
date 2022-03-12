using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
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

        [HttpPost]
        [Route("/tasks/create")]
        public async Task<TaskModel> CreateTaskAsync(
            [FromQuery] string name, [FromQuery] string text, [FromQuery] Guid assignedEmployeeId)
        {
            return await _service.CreateAsync(name, text, assignedEmployeeId);
        }

        [HttpGet]
        [Route("/tasks/getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<TaskModel> result = await _service.GetAllAsync();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/tasks/findById")]
        public async Task<IActionResult> Find([FromQuery] [Required] Guid id)
        {
            if (id == Guid.Empty) return StatusCode((int) HttpStatusCode.BadRequest);
            TaskModel result = await _service.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/tasks/findByCreationTime")]
        public IActionResult FindByCreationTime([FromQuery][Required] string creationDate)
        {
            var result = _service.GetByCreationTime(creationDate).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("/tasks/findByLastChangeTime")]
        public IActionResult FindByLastChangeTime([FromQuery][Required] string changeDate)
        {
            var result = _service.GetByLastChangeTime(changeDate).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("/tasks/findAssignedByEmployeeId")]
        public IActionResult GetAssignedByEmployeeId([FromQuery][Required] Guid employeeId)
        {
            var result = _service.GetAssignedByEmployeeId(employeeId).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("/tasks/findChangedByEmployeeId")]
        public IActionResult GetChangedByEmployeeId([FromQuery][Required] Guid employeeId)
        {
            var result = _service.GetChangedByEmployeeId(employeeId).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("/tasks/findSubordinatesTasks")]
        public IActionResult GetAllSubordinatesTasks([FromQuery][Required] Guid employeeId)
        {
            var result = _service.GetAllSubordinatesTasks(employeeId).ToList();
            return Ok(result);
        }

        [HttpDelete]
        [Route("/tasks/deleteById")]
        public async Task<IActionResult> DeleteByIdAsync([FromQuery][Required] Guid id)
        {
            await _service.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPut]
        [Route("/tasks/changeCondition")]
        public async Task<IActionResult> ChangeConditionAsync(
            [FromQuery][Required] Guid id, [FromQuery][Required] string condition)
        {
            await _service.ChangeConditionAsync(id, condition);
            return Ok();
        }

        [HttpPut]
        [Route("/tasks/changeAssignedEmployee")]
        public async Task<IActionResult> ChangeAssignedEmployeeAsync(
            [FromQuery][Required] Guid newAssignedEmployeeId, [FromQuery][Required] Guid taskId)
        {
            await _service.ChangeAssignedEmployeeAsync(newAssignedEmployeeId, taskId);
            return Ok();
        }

        [HttpPatch]
        [Route("/tasks/addComment")]
        public async Task<IActionResult> AddCommentAsync([FromQuery] [Required] Guid employeeId,
            [FromQuery] [Required] Guid taskId, [FromQuery] [Required] string message)
        {
            await _service.AddCommentAsync(employeeId, taskId, message);
            return Ok();
        }
    }
}