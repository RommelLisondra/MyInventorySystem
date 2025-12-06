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
    public class ApprovalHistoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ApprovalHistoryController));

        public ApprovalHistoryController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/ApprovalHistory
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApprovalHistoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var approvalHistory = await _serviceManager.ApprovalHistoryService.GetAll();
                return Ok(approvalHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Historys", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ApprovalHistoryModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var approvalHistory = await _serviceManager.ApprovalHistoryService.GetAllPaged(pageNumber, pageSize);
                return Ok(approvalHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ApprovalHistory/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApprovalHistoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var approvalHistory = await _serviceManager.ApprovalHistoryService.GetById(id);
                if (approvalHistory == null)
                    return NotFound();

                return Ok(approvalHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval History by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/ApprovalHistory
        [HttpPost]
        [ProducesResponseType(typeof(ApprovalHistoryModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ApprovalHistoryModel approvalHistoryModel)
        {
            if (approvalHistoryModel == null)
                return BadRequest("Approval History data cannot be null.");

            try
            {
                var approvalHistoryDto = _mapper.Map<ApprovalHistoryDto>(approvalHistoryModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ApprovalHistoryService.CreateApprovalHistory(approvalHistoryDto, createdBy);

                var returnedId = approvalHistoryDto?.id ?? 0;

                var createdModel = _mapper.Map<ApprovalHistoryModel>(approvalHistoryDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating Approval History", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/ApprovalHistory/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ApprovalHistoryModel approvalHistoryModel)
        {
            if (approvalHistoryModel == null || id != approvalHistoryModel.id)
                return BadRequest("Invalid Approval History data.");

            try
            {
                var approvalHistoryDto = _mapper.Map<ApprovalHistoryDto>(approvalHistoryModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ApprovalHistoryService.UpdateApprovalHistory(approvalHistoryDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval History with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/ApprovalHistory/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var approvalHistory = await _serviceManager.ApprovalHistoryService.GetById(id);
                if (approvalHistory == null)
                    return NotFound();

                // Kung meron kang Delete method sa IApprovalHistoryService, call it here.
                // await _ApprovalHistoryService.DeleteApprovalHistory(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting Approval History with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ApprovalHistory/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ApprovalHistoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var approvalHistory = await _serviceManager.ApprovalHistoryService.SearchApprovalHistorysByKeywordAsync(keyword);
                return Ok(approvalHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Historys", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
