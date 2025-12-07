using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Sample.ApplicationService;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.WebAPI;
using Sample.WebAPI.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Sample.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemImageController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemImageController));

        public ItemImageController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/itemImage
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemImageModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var itemImage = await _serviceManager.ItemImageService.GetAll();
                return Ok(itemImage);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching itemImage", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ItemImageModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var itemImage = await _serviceManager.ItemImageService.GetAllPaged(pageNumber, pageSize);
                return Ok(itemImage);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/itemImage/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemImageModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var itemImage = await _serviceManager.ItemImageService.GetById(id);
                if (itemImage == null)
                    return NotFound();

                return Ok(itemImage);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching itemImage by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/itemImage
        [HttpPost]
        [ProducesResponseType(typeof(ItemImageModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ItemImageModel itemImageModel)
        {
            if (itemImageModel == null)
                return BadRequest("itemImage data cannot be null.");

            try
            {
                var itemImageDto = _mapper.Map<ItemImageDto>(itemImageModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemImageService.CreateItemImage(itemImageDto, createdBy);

                var returnedId = itemImageDto?.id ?? 0;

                var createdModel = _mapper.Map<ItemImageModel>(itemImageDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating itemImage", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/itemImage/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemImageModel itemImageModel)
        {
            if (itemImageModel == null || id != itemImageModel.id)
                return BadRequest("Invalid itemImage data.");

            try
            {
                var itemImageDto = _mapper.Map<ItemImageDto>(itemImageModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemImageService.UpdateItemImage(itemImageDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating itemImage with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/itemImage/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var itemImage = await _serviceManager.ItemImageService.GetById(id);
                if (itemImage == null)
                    return NotFound();

                // Kung meron kang Delete method sa IItemImageService, call it here.
                // await _ItemImageService.DeleteitemImage(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting itemImage with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/itemImage/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ItemImageModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? name)
        {
            try
            {
                var itemImage = await _serviceManager.ItemImageService.SearchItemsAsync(name);
                return Ok(itemImage);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching itemImage", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
