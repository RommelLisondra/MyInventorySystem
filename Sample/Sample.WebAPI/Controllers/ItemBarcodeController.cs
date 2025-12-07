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
    public class ItemBarcodeController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemBarcodeController));

        public ItemBarcodeController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/ItemBarcode
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemBarcodeModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var itemBarcodes = await _serviceManager.ItemBarcodeService.GetAll();
                return Ok(itemBarcodes);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ItemBarcodeModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var itemBarcodes = await _serviceManager.ItemBarcodeService.GetAllPaged(pageNumber, pageSize);
                return Ok(itemBarcodes);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ItemBarcode/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemBarcodeModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var itemBarcode = await _serviceManager.ItemBarcodeService.GetById(id);
                if (itemBarcode == null)
                    return NotFound();

                return Ok(itemBarcode);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval Flow by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/ItemBarcode
        [HttpPost]
        [ProducesResponseType(typeof(ItemBarcodeModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ItemBarcodeModel itemBarcodeModel)
        {
            if (itemBarcodeModel == null)
                return BadRequest("Approval Flow data cannot be null.");

            try
            {
                var itemBarcodeDto = _mapper.Map<ItemBarcodeDto>(itemBarcodeModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemBarcodeService.CreateItemBarcode(itemBarcodeDto, createdBy);

                var returnedId = itemBarcodeDto?.id ?? 0;

                var createdModel = _mapper.Map<ItemBarcodeModel>(itemBarcodeDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating ItemBarcode", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/ItemBarcode/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemBarcodeModel itemBarcodeModel)
        {
            if (itemBarcodeModel == null || id != itemBarcodeModel.id)
                return BadRequest("Invalid ItemBarcode data.");

            try
            {
                var itemBarcodeDto = _mapper.Map<ItemBarcodeDto>(itemBarcodeModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemBarcodeService.UpdateItemBarcode(itemBarcodeDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/ItemBarcode/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var itemBarcode = await _serviceManager.ItemBarcodeService.GetById(id);
                if (itemBarcode == null)
                    return NotFound();

                // Kung meron kang Delete method sa IItemBarcodeService, call it here.
                // await _ItemBarcodeService.DeleteItemBarcode(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting ItemBarcode with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ItemBarcode/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ItemBarcodeModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var itemBarcode = await _serviceManager.ItemBarcodeService.SearchItemBarcodesByKeywordAsync(keyword);
                return Ok(itemBarcode);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
