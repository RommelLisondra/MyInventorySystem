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
    public class SystemSettingsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SystemSettingsController));

        public SystemSettingsController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/SystemSettings
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SystemSettingModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var systemSettings = await _serviceManager.SystemSettingsService.GetAll();
                return Ok(systemSettings);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching SystemSettings", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<SystemSettingModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var systemSettings = await _serviceManager.SystemSettingsService.GetAllPaged(pageNumber, pageSize);
                return Ok(systemSettings);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged systemSettings", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/SystemSettings/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SystemSettingModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var systemSettings = await _serviceManager.SystemSettingsService.GetById(id);
                if (systemSettings == null)
                    return NotFound();

                return Ok(systemSettings);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching System Settings by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/SystemSettings
        [HttpPost]
        [ProducesResponseType(typeof(SystemSettingModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SystemSettingModel systemSettingModel)
        {
            if (systemSettingModel == null)
                return BadRequest("System Settings data cannot be null.");

            try
            {
                var systemSettingDto = _mapper.Map<SystemSettingDto>(systemSettingModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SystemSettingsService.CreateSystemSetting(systemSettingDto, createdBy);

                var returnedId = systemSettingDto?.id ?? 0;

                var createdModel = _mapper.Map<SystemSettingModel>(systemSettingDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating SystemSettings", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/SystemSettings/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] SystemSettingModel systemSettingModel)
        {
            if (systemSettingModel == null || id != systemSettingModel.id)
                return BadRequest("Invalid System Settings data.");

            try
            {
                var systemSettingDto = _mapper.Map<SystemSettingDto>(systemSettingModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SystemSettingsService.UpdateSystemSetting(systemSettingDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating System Settings with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/SystemSettings/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var systemSettings = await _serviceManager.SystemSettingsService.GetById(id);
                if (systemSettings == null)
                    return NotFound();

                // Kung meron kang Delete method sa ISystemSettingsService, call it here.
                // await _SystemSettingsService.DeleteSystemSettings(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting System Settings with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/SystemSettings/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<SystemSettingModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var systemSettings = await _serviceManager.SystemSettingsService.SearchSystemSettingsByKeywordAsync(keyword);
                return Ok(systemSettings);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching System Settings", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
