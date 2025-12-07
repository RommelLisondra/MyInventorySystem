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
    [Route("api/[controller]")]
    [ApiController]
    public class ItemInventoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemDetailController));

        public ItemInventoryController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/itemDetail
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemInventoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var itemDetails = await _serviceManager.ItemInventoryService.GetAll();
                return Ok(itemDetails);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching itemDetails", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ItemInventoryModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var itemDetails = await _serviceManager.ItemInventoryService.GetAllPaged(pageNumber, pageSize);
                return Ok(itemDetails);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/itemDetail/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemInventoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var itemDetail = await _serviceManager.ItemInventoryService.GetById(id);
                if (itemDetail == null)
                    return NotFound();

                return Ok(itemDetail);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching itemDetail by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/itemDetail
        [HttpPost]
        [ProducesResponseType(typeof(ItemInventoryModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ItemInventoryModel ItemInventoryModel)
        {
            if (ItemInventoryModel == null)
                return BadRequest("itemDetail data cannot be null.");

            try
            {
                var itemDetailDto = _mapper.Map<ItemInventoryDto>(ItemInventoryModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemInventoryService.CreateItemInventory(itemDetailDto, createdBy);

                var returnedId = itemDetailDto?.id ?? 0;

                var createdModel = _mapper.Map<ItemInventoryModel>(itemDetailDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating itemDetail", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/itemDetail/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemInventoryModel ItemInventoryModel)
        {
            if (ItemInventoryModel == null || id != ItemInventoryModel.id)
                return BadRequest("Invalid itemDetail data.");

            try
            {
                var itemDetailDto = _mapper.Map<ItemInventoryDto>(ItemInventoryModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemInventoryService.UpdateItemInventory(itemDetailDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating itemDetail with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/itemDetail/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var itemDetail = await _serviceManager.ItemInventoryService.GetById(id);
                if (itemDetail == null)
                    return NotFound();

                // Kung meron kang Delete method sa IItemInventoryService, call it here.
                // await _ItemInventoryService.DeleteitemDetail(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting itemDetail with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/itemDetail/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ItemInventoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var itemDetails = await _serviceManager.ItemInventoryService.SearchItemInventorysByKeywordAsync(keyword);
                return Ok(itemDetails);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching itemDetails", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
