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
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Sample.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemUnitMeasureController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemUnitMeasureController));

        public ItemUnitMeasureController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/itemUnitMeasure
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemUnitMeasureModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var itemUnitMeasures = await _serviceManager.ItemUnitMeasureService.GetAll();
                return Ok(itemUnitMeasures);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching item Unit Measures", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ItemUnitMeasureModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var itemUnitMeasures = await _serviceManager.ItemUnitMeasureService.GetAllPaged(pageNumber, pageSize);
                return Ok(itemUnitMeasures);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged item Unit Measures", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/itemUnitMeasure/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemUnitMeasureModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var itemUnitMeasure = await _serviceManager.ItemUnitMeasureService.GetById(id);
                if (itemUnitMeasure == null)
                    return NotFound();

                return Ok(itemUnitMeasure);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching item Unit Measure by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/itemUnitMeasure
        [HttpPost]
        [ProducesResponseType(typeof(ItemUnitMeasureModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ItemUnitMeasureModel itemUnitMeasureModel)
        {
            if (itemUnitMeasureModel == null)
                return BadRequest("itemUnitMeasure data cannot be null.");

            try
            {
                var itemUnitMeasureDto = _mapper.Map<ItemUnitMeasureDto>(itemUnitMeasureModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemUnitMeasureService.CreateItemUnitMeasure(itemUnitMeasureDto, createdBy);

                var returnedId = itemUnitMeasureDto?.id ?? 0;

                var createdModel = _mapper.Map<ItemUnitMeasureModel>(itemUnitMeasureDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating item Unit Measure", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/itemUnitMeasure/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemUnitMeasureModel itemUnitMeasureModel)
        {
            if (itemUnitMeasureModel == null || id != itemUnitMeasureModel.id)
                return BadRequest("Invalid item Unit Measure data.");

            try
            {
                var itemUnitMeasureDto = _mapper.Map<ItemUnitMeasureDto>(itemUnitMeasureModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemUnitMeasureService.UpdateItemUnitMeasure(itemUnitMeasureDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating item Unit Measure with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/itemUnitMeasure/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var itemUnitMeasure = await _serviceManager.ItemUnitMeasureService.GetById(id);
                if (itemUnitMeasure == null)
                    return NotFound();

                // Kung meron kang Delete method sa IItemUnitMeasureService, call it here.
                // await _ItemUnitMeasureService.DeleteitemUnitMeasure(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting item Unit Measure with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/itemUnitMeasure/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ItemUnitMeasureModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var itemUnitMeasures = await _serviceManager.ItemUnitMeasureService.SearchItemUnitMeasureAsync(keyword);
                return Ok(itemUnitMeasures);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching item Unit Measures", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
