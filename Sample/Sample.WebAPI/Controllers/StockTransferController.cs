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
    public class StockTransferController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(StockTransferController));

        public StockTransferController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/StockTransfer
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StockTransferModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var stockTransfers = await _serviceManager.StockTransferService.GetAll();
                return Ok(stockTransfers);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Stock Transfers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<StockTransferModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var stockTransfers = await _serviceManager.StockTransferService.GetAllPaged(pageNumber, pageSize);
                return Ok(stockTransfers);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged stockTransfers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/StockTransfer/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StockTransferModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var stockTransfer = await _serviceManager.StockTransferService.GetById(id);
                if (stockTransfer == null)
                    return NotFound();

                return Ok(stockTransfer);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Stock Transfer by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/StockTransfer
        [HttpPost]
        [ProducesResponseType(typeof(StockTransferModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] StockTransferModel stockTransferModel)
        {
            if (stockTransferModel == null)
                return BadRequest("Stock Transfer data cannot be null.");

            try
            {
                var stockTransferDto = _mapper.Map<StockTransferDto>(stockTransferModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.StockTransferService.CreateStockTransfer(stockTransferDto, createdBy);

                var returnedId = stockTransferDto?.id ?? 0;

                var createdModel = _mapper.Map<StockTransferModel>(stockTransferDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating Stock Transfer", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/StockTransfer/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] StockTransferModel stockTransferModel)
        {
            if (stockTransferModel == null || id != stockTransferModel.id)
                return BadRequest("Invalid Stock Transfer data.");

            try
            {
                var stockTransferDto = _mapper.Map<StockTransferDto>(stockTransferModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.StockTransferService.UpdateStockTransfer(stockTransferDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Stock Transfer with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/StockTransfer/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var stockTransfer = await _serviceManager.StockTransferService.GetById(id);
                if (stockTransfer == null)
                    return NotFound();

                // Kung meron kang Delete method sa IStockTransferService, call it here.
                // await _StockTransferService.DeleteStockTransfer(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting Stock Transfer with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/StockTransfer/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<StockTransferModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var stockTransfer = await _serviceManager.StockTransferService.SearchStockTransfersByKeywordAsync(keyword);
                return Ok(stockTransfer);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Stock Transfers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
