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
    public class ItemPriceHistoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemPriceHistoryController));

        public ItemPriceHistoryController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/ItemPriceHistory
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemPriceHistoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var itemPriceHistory = await _serviceManager.ItemPriceHistoryService.GetAll();
                return Ok(itemPriceHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ItemPriceHistoryModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var itemPriceHistory = await _serviceManager.ItemPriceHistoryService.GetAllPaged(pageNumber, pageSize);
                return Ok(itemPriceHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ItemPriceHistory/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemPriceHistoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var itemPriceHistory = await _serviceManager.ItemPriceHistoryService.GetById(id);
                if (itemPriceHistory == null)
                    return NotFound();

                return Ok(itemPriceHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval Flow by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/ItemPriceHistory
        [HttpPost]
        [ProducesResponseType(typeof(ItemPriceHistoryModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ItemPriceHistoryModel itemPriceHistoryModel)
        {
            if (itemPriceHistoryModel == null)
                return BadRequest("Approval Flow data cannot be null.");

            try
            {
                var itemPriceHistoryDto = _mapper.Map<ItemPriceHistoryDto>(itemPriceHistoryModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemPriceHistoryService.CreateItemPriceHistory(itemPriceHistoryDto, createdBy);

                var returnedId = itemPriceHistoryDto?.id ?? 0;

                var createdModel = _mapper.Map<ItemPriceHistoryModel>(itemPriceHistoryDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating ItemPriceHistory", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/ItemPriceHistory/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemPriceHistoryModel ItemPriceHistoryModel)
        {
            if (ItemPriceHistoryModel == null || id != ItemPriceHistoryModel.id)
                return BadRequest("Invalid ItemPriceHistory data.");

            try
            {
                var ItemPriceHistoryDto = _mapper.Map<ItemPriceHistoryDto>(ItemPriceHistoryModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemPriceHistoryService.UpdateItemPriceHistory(ItemPriceHistoryDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/ItemPriceHistory/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ItemPriceHistory = await _serviceManager.ItemPriceHistoryService.GetById(id);
                if (ItemPriceHistory == null)
                    return NotFound();

                // Kung meron kang Delete method sa IItemPriceHistoryService, call it here.
                // await _ItemPriceHistoryService.DeleteItemPriceHistory(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting ItemPriceHistory with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ItemPriceHistory/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ItemPriceHistoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var ItemPriceHistorys = await _serviceManager.ItemPriceHistoryService.SearchItemPriceHistorysByKeywordAsync(keyword);
                return Ok(ItemPriceHistorys);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
