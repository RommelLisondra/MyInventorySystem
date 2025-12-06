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
    public class ApprovalFlowController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ApprovalFlowController));

        public ApprovalFlowController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/ApprovalFlow
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApprovalFlowModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var approvalFlows = await _serviceManager.ApprovalFlowService.GetAll();
                return Ok(approvalFlows);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ApprovalFlowModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var approvalFlows = await _serviceManager.ApprovalFlowService.GetAllPaged(pageNumber, pageSize);
                return Ok(approvalFlows);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ApprovalFlow/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApprovalFlowModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var approvalFlow = await _serviceManager.ApprovalFlowService.GetById(id);
                if (approvalFlow == null)
                    return NotFound();

                return Ok(approvalFlow);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval Flow by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/ApprovalFlow
        [HttpPost]
        [ProducesResponseType(typeof(ApprovalFlowModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ApprovalFlowModel approvalFlowModel)
        {
            if (approvalFlowModel == null)
                return BadRequest("Approval Flow data cannot be null.");

            try
            {
                var approvalFlowDto = _mapper.Map<ApprovalFlowDto>(approvalFlowModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ApprovalFlowService.CreateApprovalFlow(approvalFlowDto, createdBy);

                var returnedId = approvalFlowDto?.id ?? 0;

                var createdModel = _mapper.Map<ApprovalFlowModel>(approvalFlowDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating ApprovalFlow", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/ApprovalFlow/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ApprovalFlowModel approvalFlowModel)
        {
            if (approvalFlowModel == null || id != approvalFlowModel.id)
                return BadRequest("Invalid ApprovalFlow data.");

            try
            {
                var approvalFlowDto = _mapper.Map<ApprovalFlowDto>(approvalFlowModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ApprovalFlowService.UpdateApprovalFlow(approvalFlowDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/ApprovalFlow/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var approvalFlow = await _serviceManager.ApprovalFlowService.GetById(id);
                if (approvalFlow == null)
                    return NotFound();

                // Kung meron kang Delete method sa IApprovalFlowService, call it here.
                // await _ApprovalFlowService.DeleteApprovalFlow(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting ApprovalFlow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ApprovalFlow/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ApprovalFlowModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var approvalFlows = await _serviceManager.ApprovalFlowService.SearchApprovalFlowsByKeywordAsync(keyword);
                return Ok(approvalFlows);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
