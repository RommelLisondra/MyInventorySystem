using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.ApplicationService;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.WebAPI.Model;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sample.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeImageController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(EmployeeController));

        public EmployeeImageController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<EmployeeImageModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var employee = await _serviceManager.EmployeeImageService.GetAllPaged(pageNumber, pageSize);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/employee/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeImageModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var employee = await _serviceManager.EmployeeImageService.GetById(id);
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
        [ProducesResponseType(typeof(EmployeeImageModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] EmployeeImageModel employeeImageModel)
        {
            if (employeeImageModel == null)
                return BadRequest("employee data cannot be null.");

            try
            {
                var employeeDto = _mapper.Map<EmployeeImageDto>(employeeImageModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.EmployeeImageService.CreateEmployeeImage(employeeDto, createdBy);

                var returnedId = employeeDto?.id ?? 0;

                var createdModel = _mapper.Map<EmployeeImageModel>(employeeDto);

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
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeImageModel employeeImageModel)
        {
            if (employeeImageModel == null || id != employeeImageModel.id)
                return BadRequest("Invalid employee data.");

            try
            {
                var employeeDto = _mapper.Map<EmployeeImageDto>(employeeImageModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.EmployeeImageService.UpdateEmployeeImage(employeeDto, editedBy);

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
                var employee = await _serviceManager.EmployeeImageService.GetById(id);
                if (employee == null)
                    return NotFound();

                // Kung meron kang Delete method sa IEmployeeImageService, call it here.
                // await _EmployeeImageService.Deleteemployee(id);

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
        [ProducesResponseType(typeof(IEnumerable<EmployeeImageModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var employees = await _serviceManager.EmployeeImageService.SearchEmployeeByKeywordAsync(keyword);
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
