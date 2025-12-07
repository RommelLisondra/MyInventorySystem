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
    public class WarehouseController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(WarehouseController));

        public WarehouseController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/warehouse
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WarehouseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var warehouses = await _serviceManager.WarehouseService.GetAll();
                return Ok(warehouses);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<WarehouseModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var pagedWarehouses = await _serviceManager.WarehouseService.GetAllPaged(pageNumber, pageSize);
                return Ok(pagedWarehouses);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/warehouse/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WarehouseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var warehouse = await _serviceManager.WarehouseService.GetById(id);
                if (warehouse == null)
                    return NotFound();

                return Ok(warehouse);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching warehouse by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/warehouse
        [HttpPost]
        [ProducesResponseType(typeof(WarehouseModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] WarehouseModel warehouseModel)
        {
            if (warehouseModel == null)
                return BadRequest("warehouse data cannot be null.");

            try
            {
                var warehouseDto = _mapper.Map<WarehouseDto>(warehouseModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.WarehouseService.CreateWarehouse(warehouseDto, createdBy);

                var returnedId = warehouseDto?.id ?? 0;

                var createdModel = _mapper.Map<WarehouseModel>(warehouseDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating warehouse", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/warehouse/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] WarehouseModel warehouseModel)
        {
            if (warehouseModel == null || id != warehouseModel.id)
                return BadRequest("Invalid warehouse data.");

            try
            {
                var warehouseDto = _mapper.Map<WarehouseDto>(warehouseModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.WarehouseService.UpdateWarehouse(warehouseDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating warehouse with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/warehouse/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var warehouse = await _serviceManager.WarehouseService.GetById(id);
                if (warehouse == null)
                    return NotFound();

                // Kung meron kang Delete method sa IWarehouseService, call it here.
                // await _WarehouseService.Deletewarehouse(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting warehouse with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/warehouse/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<WarehouseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? name)
        {
            try
            {
                var warehouses = await _serviceManager.WarehouseService.SearchWarehousesAsync(name);
                return Ok(warehouses);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
