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
    public class ItemSupplierController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemSupplierController));

        public ItemSupplierController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/itemSupplier
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemSupplierModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var itemSuppliers = await _serviceManager.ItemSupplierService.GetAll();
                return Ok(itemSuppliers);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching itemSuppliers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ItemSupplierModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var itemSuppliers = await _serviceManager.ItemSupplierService.GetAllPaged(pageNumber, pageSize);
                return Ok(itemSuppliers);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/itemSupplier/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemSupplierModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var itemSupplier = await _serviceManager.ItemSupplierService.GetById(id);
                if (itemSupplier == null)
                    return NotFound();

                return Ok(itemSupplier);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching itemSupplier by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/itemSupplier
        [HttpPost]
        [ProducesResponseType(typeof(ItemSupplierModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ItemSupplierModel itemSupplierModel)
        {
            if (itemSupplierModel == null)
                return BadRequest("itemSupplier data cannot be null.");

            try
            {
                var itemSupplierDto = _mapper.Map<ItemSupplierDto>(itemSupplierModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemSupplierService.CreateItemSupplier(itemSupplierDto, createdBy);

                var returnedId = itemSupplierDto?.id ?? 0;

                var createdModel = _mapper.Map<ItemSupplierModel>(itemSupplierDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating itemSupplier", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/itemSupplier/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemSupplierModel itemSupplierModel)
        {
            if (itemSupplierModel == null || id != itemSupplierModel.id)
                return BadRequest("Invalid itemSupplier data.");

            try
            {
                var itemSupplierDto = _mapper.Map<ItemSupplierDto>(itemSupplierModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ItemSupplierService.UpdateItemSupplier(itemSupplierDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating itemSupplier with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/itemSupplier/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var itemSupplier = await _serviceManager.ItemSupplierService.GetById(id);
                if (itemSupplier == null)
                    return NotFound();

                // Kung meron kang Delete method sa IItemSupplierService, call it here.
                // await _ItemSupplierService.DeleteitemSupplier(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting itemSupplier with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/itemSupplier/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ItemSupplierModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var itemSuppliers = await _serviceManager.ItemSupplierService.SearchItemSupplierKeywordAsync(keyword);
                return Ok(itemSuppliers);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching itemSuppliers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
