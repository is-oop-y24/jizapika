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

        /*[HttpPost]
        [Route("/employees/creationTeamLead")]
        public async Task<Employee> CreateTeamLeadAsync([FromQuery][Required] string name)
        {
            return await _service.CreateNewTeamLeadAsync(name);
        }

        [HttpPost]
        [Route("/employees/creation")]
        public async Task<Employee> CreateAsync([FromQuery][Required] string name, [FromQuery][Required] Guid supervisorId)
        {
            return await _service.CreateAsync(name, supervisorId);
        }*/

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

        /*[HttpDelete]
        [Route("/employees/deleteById")]
        public async Task<IActionResult> DeleteByIdAsync([FromQuery][Required] Guid id)
        {
            await _service.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpDelete]
        [Route("/employees/deleteByName")]
        public async Task<IActionResult> DeleteByNameAsync([FromQuery][Required] string name)
        {
            await _service.DeleteByNameAsync(name);
            return Ok();
        }

        [HttpPut]
        [Route("/employees/updateSupervisorAsync")]
        public async Task<IActionResult> UpdateSupervisorAsync([FromQuery][Required] Guid updatingEmployeeId,
            [FromQuery][Required] Guid newSupervisorId)
        {
            await _service.UpdateSupervisorAsync(updatingEmployeeId, newSupervisorId);
            return Ok();
        }*/
    }
}