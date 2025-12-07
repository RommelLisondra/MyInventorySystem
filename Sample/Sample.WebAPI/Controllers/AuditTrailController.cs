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
    public class AuditTrailController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(AuditTrailController));

        public AuditTrailController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/AuditTrail
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AuditTrailModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var auditTrails = await _serviceManager.AuditTrailService.GetAll();
                return Ok(auditTrails);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Audit Trails", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<AuditTrailModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var pagedauditTrails = await _serviceManager.AuditTrailService.GetAllPaged(pageNumber, pageSize);
                return Ok(pagedauditTrails);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged auditTrails", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/AuditTrail/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AuditTrailModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var auditTrail = await _serviceManager.AuditTrailService.GetById(id);
                if (auditTrail == null)
                    return NotFound();

                return Ok(auditTrail);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Audit Trail by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/AuditTrail
        [HttpPost]
        [ProducesResponseType(typeof(AuditTrailModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AuditTrailModel auditTrailModel)
        {
            if (auditTrailModel == null)
                return BadRequest("AuditTrail data cannot be null.");

            try
            {
                var auditTrailDto = _mapper.Map<AuditTrailDto>(auditTrailModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.AuditTrailService.CreateAuditTrail(auditTrailDto, createdBy);

                var returnedId = auditTrailDto?.id ?? 0;

                var createdModel = _mapper.Map<AuditTrailModel>(auditTrailDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating AuditTrail", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/AuditTrail/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] AuditTrailModel auditTrailModel)
        {
            if (auditTrailModel == null || id != auditTrailModel.id)
                return BadRequest("Invalid AuditTrail data.");

            try
            {
                var auditTrailDto = _mapper.Map<AuditTrailDto>(auditTrailModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.AuditTrailService.UpdateAuditTrail(auditTrailDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating AuditTrail with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/AuditTrail/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var auditTrail = await _serviceManager.AuditTrailService.GetById(id);
                if (auditTrail == null)
                    return NotFound();

                // Kung meron kang Delete method sa IAuditTrailService, call it here.
                // await _AuditTrailService.DeleteAuditTrail(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting AuditTrail with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/AuditTrail/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<AuditTrailModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var auditTrail = await _serviceManager.AuditTrailService.SearchAuditTrailsByKeywordAsync(keyword);
                return Ok(auditTrail);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching auditTrail", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
