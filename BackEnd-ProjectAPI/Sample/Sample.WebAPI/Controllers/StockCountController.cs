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
    public class StockCountController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(StockCountController));

        public StockCountController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/StockCount
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StockCountMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var stockCount = await _serviceManager.StockCountService.GetAll();
                return Ok(stockCount);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Stock Transfers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<StockCountMasterModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var stockCount = await _serviceManager.StockCountService.GetAllPaged(pageNumber, pageSize);
                return Ok(stockCount);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged StockCounts", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/StockCount/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StockCountMasterModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var stockCount = await _serviceManager.StockCountService.GetById(id);
                if (stockCount == null)
                    return NotFound();

                return Ok(stockCount);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Stock Transfer by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/StockCount
        [HttpPost]
        [ProducesResponseType(typeof(StockCountMasterModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] StockCountMasterModel stockCountMasterModel)
        {
            if (stockCountMasterModel == null)
                return BadRequest("Stock Transfer data cannot be null.");

            try
            {
                var stockCountDto = _mapper.Map<StockCountMasterDto>(stockCountMasterModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.StockCountService.CreateStockCount(stockCountDto, createdBy);

                var returnedId = stockCountDto?.id ?? 0;

                var createdModel = _mapper.Map<StockCountMasterModel>(stockCountDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating Stock Transfer", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/StockCount/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] StockCountMasterModel stockCountMasterModel)
        {
            if (stockCountMasterModel == null || id != stockCountMasterModel.id)
                return BadRequest("Invalid Stock Transfer data.");

            try
            {
                var stockCountDto = _mapper.Map<StockCountMasterDto>(stockCountMasterModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.StockCountService.UpdateStockCount(stockCountDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Stock Transfer with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/StockCount/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var stockCount = await _serviceManager.StockCountService.GetById(id);
                if (stockCount == null)
                    return NotFound();

                // Kung meron kang Delete method sa IStockCountService, call it here.
                // await _StockCountService.DeleteStockCount(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting Stock Transfer with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/StockCount/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<StockCountMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var stockCount = await _serviceManager.StockCountService.SearchStockCountMastersByKeywordAsync(keyword);
                return Ok(stockCount);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Stock Transfers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
