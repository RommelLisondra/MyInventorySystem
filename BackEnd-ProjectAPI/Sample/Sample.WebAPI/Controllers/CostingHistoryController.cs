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
    public class CostingHistoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(CostingHistoryController));

        public CostingHistoryController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/CostingHistory
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CostingHistoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var costingHistory = await _serviceManager.CostingHistoryService.GetAll();
                return Ok(costingHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<CostingHistoryModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var pagedcostingHistory = await _serviceManager.CostingHistoryService.GetAllPaged(pageNumber, pageSize);
                return Ok(pagedcostingHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/CostingHistory/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CostingHistoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var costingHistory = await _serviceManager.CostingHistoryService.GetById(id);
                if (costingHistory == null)
                    return NotFound();

                return Ok(costingHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval Flow by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/CostingHistory
        [HttpPost]
        [ProducesResponseType(typeof(CostingHistoryModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CostingHistoryModel costingHistoryModel)
        {
            if (costingHistoryModel == null)
                return BadRequest("Approval Flow data cannot be null.");

            try
            {
                var costingHistoryDto = _mapper.Map<CostingHistoryDto>(costingHistoryModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.CostingHistoryService.CreateCostingHistory(costingHistoryDto, createdBy);

                var returnedId = costingHistoryDto?.id ?? 0;

                var createdModel = _mapper.Map<CostingHistoryModel>(costingHistoryDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating CostingHistory", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/CostingHistory/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] CostingHistoryModel costingHistoryModel)
        {
            if (costingHistoryModel == null || id != costingHistoryModel.id)
                return BadRequest("Invalid CostingHistory data.");

            try
            {
                var costingHistoryDto = _mapper.Map<CostingHistoryDto>(costingHistoryModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.CostingHistoryService.UpdateCostingHistory(costingHistoryDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/CostingHistory/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var costingHistory = await _serviceManager.CostingHistoryService.GetById(id);
                if (costingHistory == null)
                    return NotFound();

                // Kung meron kang Delete method sa ICostingHistoryService, call it here.
                // await _CostingHistoryService.DeleteCostingHistory(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting CostingHistory with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/CostingHistory/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<CostingHistoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var costingHistory = await _serviceManager.CostingHistoryService.SearchCostingHistorysByKeywordAsync(keyword);
                return Ok(costingHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
