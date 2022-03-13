using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/reports/create")]
        public async Task<IActionResult> AppointReportAsync([FromQuery][Required] string deadLineTime,
            [FromQuery][Required] Guid assignedEmployeeId)
        {
            Report result = await _service.AppointReportAsync(deadLineTime, assignedEmployeeId);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/tasks/findByEmployee")]
        public IActionResult GetByEmployeeId([FromQuery][Required] Guid employeeId)
        {
            IEnumerable<Report> result = _service.GetByEmployeeId(employeeId);
            return Ok(result);
        }

        [HttpGet]
        [Route("/tasks/findReadyReportsOfSubordinates")]
        public IActionResult GetReadyReportsOfSubordinates([FromQuery][Required] Guid employeeId)
        {
            IEnumerable<Report> result = _service.GetReadyReportsOfSubordinates(employeeId);
            return Ok(result);
        }

        [HttpGet]
        [Route("/tasks/findUnfinishedReportsOfSubordinates")]
        public IActionResult GetUnfinishedReportsOfSubordinates([FromQuery][Required] Guid employeeId)
        {
            IEnumerable<Report> result = _service.GetUnfinishedReportsOfSubordinates(employeeId);
            return Ok(result);
        }

        [HttpPut]
        [Route("/tasks/fillReport")]
        public async Task<IActionResult> FillReportAsync(
            [FromQuery][Required] Guid reportId, [FromQuery][Required] string text)
        {
            await _service.FillReportAsync(reportId, text);
            return Ok();
        }
    }
}