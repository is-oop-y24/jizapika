using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpPost]
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
        }

        [HttpGet]
        [Route("/employees/getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<Employee> result = await _service.GetAllAsync();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/employees/findById")]
        public async Task<IActionResult> Find([FromQuery] [Required] Guid id)
        {
            if (id == Guid.Empty) return StatusCode((int) HttpStatusCode.BadRequest);
            Employee result = await _service.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/employees/findByName")]
        public async Task<IActionResult> Find([FromQuery][Required] string name)
        {
            Employee result = await _service.GetByNameAsync(name);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpDelete]
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
        }
    }
}