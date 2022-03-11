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
        public async Task<Employee> CreateTeamLeadAsync([FromQuery] [Required] string name)
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
        public IActionResult GetAll()
        {
            IEnumerable<Employee> result = _service.GetAll();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/employees/find")]
        public async Task<IActionResult> Find([FromQuery][Required] Guid id)
        {
            if (id == Guid.Empty) return StatusCode((int) HttpStatusCode.BadRequest);
            Employee result = await _service.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}