using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.ApplicationService;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.WebAPI.Model;
using System.Net;

namespace Sample.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSalesRefController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(EmployeeController));

        public EmployeeSalesRefController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/employee
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeSalesRefModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employees = await _serviceManager.EmployeeSalesRefService.GetAll();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching employees", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<EmployeeSalesRefModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var employees = await _serviceManager.EmployeeSalesRefService.GetAllPaged(pageNumber, pageSize);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged employees", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/employee/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeSalesRefModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var employee = await _serviceManager.EmployeeSalesRefService.GetById(id);
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
        [ProducesResponseType(typeof(EmployeeSalesRefModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] EmployeeSalesRefModel employeeSalesRefModel)
        {
            if (employeeSalesRefModel == null)
                return BadRequest("employee data cannot be null.");

            try
            {
                var employeeDto = _mapper.Map<EmployeeSalesRefDto>(employeeSalesRefModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.EmployeeSalesRefService.CreateEmployeeSalesRef(employeeDto, createdBy);

                var returnedId = employeeDto?.id ?? 0;

                var createdModel = _mapper.Map<EmployeeSalesRefModel>(employeeDto);

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
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeSalesRefModel employeeSalesRefModel)
        {
            if (employeeSalesRefModel == null || id != employeeSalesRefModel.id)
                return BadRequest("Invalid employee data.");

            try
            {
                var employeeDto = _mapper.Map<EmployeeSalesRefDto>(employeeSalesRefModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.EmployeeSalesRefService.UpdateEmployeeSalesRef(employeeDto, editedBy);

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
                var employee = await _serviceManager.EmployeeSalesRefService.GetById(id);
                if (employee == null)
                    return NotFound();

                // Kung meron kang Delete method sa IEmployeeSalesRefService, call it here.
                // await _EmployeeSalesRefService.Deleteemployee(id);

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
        [ProducesResponseType(typeof(IEnumerable<EmployeeSalesRefModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var employees = await _serviceManager.EmployeeSalesRefService.SearchEmployeeSalesRefsByKeywordAsync(keyword);
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
