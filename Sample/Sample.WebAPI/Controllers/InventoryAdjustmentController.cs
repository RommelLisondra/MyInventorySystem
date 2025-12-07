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
    public class InventoryAdjustmentController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(InventoryAdjustmentController));

        public InventoryAdjustmentController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/InventoryAdjustment
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InventoryAdjustmentModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var inventoryAdjustments = await _serviceManager.InventoryAdjustmentService.GetAll();
                return Ok(inventoryAdjustments);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching InventoryAdjustments", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<InventoryAdjustmentModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var inventoryAdjustments = await _serviceManager.InventoryAdjustmentService.GetAllPaged(pageNumber, pageSize);
                return Ok(inventoryAdjustments);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/InventoryAdjustment/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InventoryAdjustmentModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var inventoryAdjustment = await _serviceManager.InventoryAdjustmentService.GetById(id);
                if (inventoryAdjustment == null)
                    return NotFound();

                return Ok(inventoryAdjustment);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching InventoryAdjustment by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/InventoryAdjustment
        [HttpPost]
        [ProducesResponseType(typeof(InventoryAdjustmentModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] InventoryAdjustmentModel inventoryAdjustmentModel)
        {
            if (inventoryAdjustmentModel == null)
                return BadRequest("InventoryAdjustment data cannot be null.");

            try
            {
                var inventoryAdjustmentDto = _mapper.Map<InventoryAdjustmentDto>(inventoryAdjustmentModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.InventoryAdjustmentService.CreateInventoryAdjustment(inventoryAdjustmentDto, createdBy);

                var returnedId = inventoryAdjustmentDto?.id ?? 0;

                var createdModel = _mapper.Map<InventoryAdjustmentModel>(inventoryAdjustmentDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating InventoryAdjustment", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/InventoryAdjustment/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] InventoryAdjustmentModel inventoryAdjustmentModel)
        {
            if (inventoryAdjustmentModel == null || id != inventoryAdjustmentModel.id)
                return BadRequest("Invalid InventoryAdjustment data.");

            try
            {
                var inventoryAdjustmentDto = _mapper.Map<InventoryAdjustmentDto>(inventoryAdjustmentModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.InventoryAdjustmentService.UpdateInventoryAdjustment(inventoryAdjustmentDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating InventoryAdjustment with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/InventoryAdjustment/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var inventoryAdjustment = await _serviceManager.InventoryAdjustmentService.GetById(id);
                if (inventoryAdjustment == null)
                    return NotFound();

                // Kung meron kang Delete method sa IInventoryAdjustmentService, call it here.
                // await _InventoryAdjustmentService.DeleteInventoryAdjustment(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting InventoryAdjustment with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/InventoryAdjustment/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<InventoryAdjustmentModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var inventoryAdjustment = await _serviceManager.InventoryAdjustmentService.SearchInventoryAdjustmentsByKeywordAsync(keyword);
                return Ok(inventoryAdjustment);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching InventoryAdjustments", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
