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
    public class SystemLogsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SystemLogsController));

        public SystemLogsController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/SystemLogs
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SystemLogModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var systemLogs = await _serviceManager.SystemLogsService.GetAll();
                return Ok(systemLogs);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching SystemLogs", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<SystemLogModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var systemLogs = await _serviceManager.SystemLogsService.GetAllPaged(pageNumber, pageSize);
                return Ok(systemLogs);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged systemLogs", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/SystemLogs/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SystemLogModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var systemLogs = await _serviceManager.SystemLogsService.GetById(id);
                if (systemLogs == null)
                    return NotFound();

                return Ok(systemLogs);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching SystemLogs by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/SystemLogs
        [HttpPost]
        [ProducesResponseType(typeof(SystemLogModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SystemLogModel systemLogsModel)
        {
            if (systemLogsModel == null)
                return BadRequest("SystemLogs data cannot be null.");

            try
            {
                var systemLogsDto = _mapper.Map<SystemLogDto>(systemLogsModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SystemLogsService.CreateSystemLog(systemLogsDto, createdBy);

                var returnedId = systemLogsDto?.id ?? 0;

                var createdModel = _mapper.Map<SystemLogModel>(systemLogsDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating SystemLogs", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/SystemLogs/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] SystemLogModel systemLogsModel)
        {
            if (systemLogsModel == null || id != systemLogsModel.id)
                return BadRequest("Invalid SystemLogs data.");

            try
            {
                var systemLogsDto = _mapper.Map<SystemLogDto>(systemLogsModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SystemLogsService.UpdateSystemLog(systemLogsDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating SystemLogs with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/SystemLogs/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var systemLogs = await _serviceManager.SystemLogsService.GetById(id);
                if (systemLogs == null)
                    return NotFound();

                // Kung meron kang Delete method sa ISystemLogsService, call it here.
                // await _SystemLogsService.DeleteSystemLogs(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting SystemLogs with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/SystemLogs/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<SystemLogModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var systemLogs = await _serviceManager.SystemLogsService.SearchSystemLogsByKeywordAsync(keyword);
                return Ok(systemLogs);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching SystemLogs", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
