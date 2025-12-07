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
    public class ItemDetailController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemDetailController));

        public ItemDetailController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/itemDetail
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemDetailModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var itemDetails = await _serviceManager.ItemDetailService.GetAll();
                return Ok(itemDetails);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching itemDetails", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ItemDetailModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var itemDetails = await _serviceManager.ItemDetailService.GetAllPaged(pageNumber, pageSize);
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
        [ProducesResponseType(typeof(ItemDetailModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var itemDetail = await _serviceManager.ItemDetailService.GetById(id);
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
        [ProducesResponseType(typeof(ItemDetailModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ItemDetailModel itemDetailModel)
        {
            if (itemDetailModel == null)
                return BadRequest("itemDetail data cannot be null.");

            try
            {
                var itemDetailDto = _mapper.Map<ItemDetailDto>(itemDetailModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemDetailService.CreateItem(itemDetailDto, createdBy);

                var returnedId = itemDetailDto?.id ?? 0;

                var createdModel = _mapper.Map<ItemDetailModel>(itemDetailDto);

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
        public async Task<IActionResult> Update(int id, [FromBody] ItemDetailModel itemDetailModel)
        {
            if (itemDetailModel == null || id != itemDetailModel.id)
                return BadRequest("Invalid itemDetail data.");

            try
            {
                var itemDetailDto = _mapper.Map<ItemDetailDto>(itemDetailModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemDetailService.UpdateItem(itemDetailDto, editedBy);

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
                var itemDetail = await _serviceManager.ItemDetailService.GetById(id);
                if (itemDetail == null)
                    return NotFound();

                // Kung meron kang Delete method sa IitemDetailService, call it here.
                // await _itemDetailService.DeleteitemDetail(id);

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
        [ProducesResponseType(typeof(IEnumerable<ItemDetailModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var itemDetails = await _serviceManager.ItemDetailService.SearchItemsByKeywordAsync(keyword);
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
