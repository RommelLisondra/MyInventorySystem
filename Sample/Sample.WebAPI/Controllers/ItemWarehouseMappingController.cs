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
    public class ItemWarehouseMappingController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemWarehouseMappingController));

        public ItemWarehouseMappingController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/ItemWarehouseMapping
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemWarehouseMappingModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var itemWarehouseMapping = await _serviceManager.ItemWarehouseMappingService.GetAll();
                return Ok(itemWarehouseMapping);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ItemWarehouseMappingModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var itemWarehouseMapping = await _serviceManager.ItemWarehouseMappingService.GetAllPaged(pageNumber, pageSize);
                return Ok(itemWarehouseMapping);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ItemWarehouseMapping/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemWarehouseMappingModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var itemWarehouseMapping = await _serviceManager.ItemWarehouseMappingService.GetById(id);
                if (itemWarehouseMapping == null)
                    return NotFound();

                return Ok(itemWarehouseMapping);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval Flow by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/ItemWarehouseMapping
        [HttpPost]
        [ProducesResponseType(typeof(ItemWarehouseMappingModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ItemWarehouseMappingModel itemWarehouseMappingModel)
        {
            if (itemWarehouseMappingModel == null)
                return BadRequest("Approval Flow data cannot be null.");

            try
            {
                var itemWarehouseMappingDto = _mapper.Map<ItemWarehouseMappingDto>(itemWarehouseMappingModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemWarehouseMappingService.CreateItemWarehouseMapping(itemWarehouseMappingDto, createdBy);

                var returnedId = itemWarehouseMappingDto?.id ?? 0;

                var createdModel = _mapper.Map<ItemWarehouseMappingModel>(itemWarehouseMappingDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating ItemWarehouseMapping", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/ItemWarehouseMapping/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemWarehouseMappingModel itemWarehouseMappingModel)
        {
            if (itemWarehouseMappingModel == null || id != itemWarehouseMappingModel.id)
                return BadRequest("Invalid ItemWarehouseMapping data.");

            try
            {
                var itemWarehouseMappingDto = _mapper.Map<ItemWarehouseMappingDto>(itemWarehouseMappingModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemWarehouseMappingService.UpdateItemWarehouseMapping(itemWarehouseMappingDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/ItemWarehouseMapping/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var itemWarehouseMapping = await _serviceManager.ItemWarehouseMappingService.GetById(id);
                if (itemWarehouseMapping == null)
                    return NotFound();

                // Kung meron kang Delete method sa IItemWarehouseMappingService, call it here.
                // await _ItemWarehouseMappingService.DeleteItemWarehouseMapping(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting ItemWarehouseMapping with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ItemWarehouseMapping/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ItemWarehouseMappingModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var itemWarehouseMappings = await _serviceManager.ItemWarehouseMappingService.SearchItemWarehouseMappingsByKeywordAsync(keyword);
                return Ok(itemWarehouseMappings);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
