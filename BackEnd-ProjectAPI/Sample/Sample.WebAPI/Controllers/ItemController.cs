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
    public class ItemController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemController));

        public ItemController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/item
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var items = await _serviceManager.ItemService.GetAll();
                return Ok(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching items", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ItemModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var items = await _serviceManager.ItemService.GetAllPaged(pageNumber, pageSize);
                return Ok(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/item/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _serviceManager.ItemService.GetById(id);
                if (item == null)
                    return NotFound();

                return Ok(item);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching item by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/item
        [HttpPost]
        [ProducesResponseType(typeof(ItemModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ItemModel itemModel)
        {
            if (itemModel == null)
                return BadRequest("item data cannot be null.");

            try
            {
                var itemDto = _mapper.Map<ItemDto>(itemModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemService.CreateItem(itemDto, createdBy);

                var returnedId = itemDto?.id ?? 0;

                var createdModel = _mapper.Map<ItemModel>(itemDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating item", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/item/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemModel itemModel)
        {
            if (itemModel == null || id != itemModel.id)
                return BadRequest("Invalid item data.");

            try
            {
                var itemDto = _mapper.Map<ItemDto>(itemModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemService.UpdateItem(itemDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating item with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/item/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _serviceManager.ItemService.GetById(id);
                if (item == null)
                    return NotFound();

                // Kung meron kang Delete method sa IemployeeService, call it here.
                // await _employeeService.Deleteemployee(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting item with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/item/search?brandName=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ItemModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? brandName)
        {
            try
            {
                var items = await _serviceManager.ItemService.SearchItemsAsync(brandName);
                return Ok(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching item", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
