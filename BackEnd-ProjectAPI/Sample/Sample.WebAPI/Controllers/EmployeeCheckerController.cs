using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Sample.ApplicationService;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.WebAPI;
using Sample.WebAPI.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Sample.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCheckerController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(EmployeeController));

        public EmployeeCheckerController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/employee
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeCheckerModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employees = await _serviceManager.EmployeeCheckerService.GetAll();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching employees", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<EmployeeCheckerModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var employees = await _serviceManager.EmployeeCheckerService.GetAllPaged(pageNumber, pageSize);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/employee/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeCheckerModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var employee = await _serviceManager.EmployeeCheckerService.GetById(id);
                if (employee == null)
                    return NotFound();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching employee by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/employee
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeCheckerModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] EmployeeCheckerModel employeeCheckerModel)
        {
            if (employeeCheckerModel == null)
                return BadRequest("employee data cannot be null.");

            try
            {
                var employeeDto = _mapper.Map<EmployeeCheckerDto>(employeeCheckerModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.EmployeeCheckerService.CreateEmployeeChecker(employeeDto, createdBy);

                var returnedId = employeeDto?.id ?? 0;

                var createdModel = _mapper.Map<EmployeeCheckerModel>(employeeDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating employee", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/employee/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeCheckerModel employeeCheckerModel)
        {
            if (employeeCheckerModel == null || id != employeeCheckerModel.id)
                return BadRequest("Invalid employee data.");

            try
            {
                var employeeDto = _mapper.Map<EmployeeCheckerDto>(employeeCheckerModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.EmployeeCheckerService.UpdateEmployeeChecker(employeeDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating employee with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/employee/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employee = await _serviceManager.EmployeeCheckerService.GetById(id);
                if (employee == null)
                    return NotFound();

                // Kung meron kang Delete method sa IEmployeeCheckerService, call it here.
                // await _EmployeeCheckerService.Deleteemployee(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting employee with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/employee/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeCheckerModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var employees = await _serviceManager.EmployeeCheckerService.SearchEmployeeCheckersByKeywordAsync(keyword);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching employees", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
