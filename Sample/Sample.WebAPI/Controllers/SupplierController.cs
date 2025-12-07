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
    public class SupplierController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SupplierController));

        public SupplierController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/supplier
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SupplierModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var supplier = await _serviceManager.SupplierService.GetAll();
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching suppliers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<SupplierModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var supplier = await _serviceManager.SupplierService.GetAllPaged(pageNumber, pageSize);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged supplier", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/supplier/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SupplierModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var supplier = await _serviceManager.SupplierService.GetById(id);
                if (supplier == null)
                    return NotFound();

                return Ok(supplier);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching supplier by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/supplier
        [HttpPost]
        [ProducesResponseType(typeof(SupplierModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SupplierModel supplierModel)
        {
            if (supplierModel == null)
                return BadRequest("supplier data cannot be null.");

            try
            {
                var supplierDto = _mapper.Map<SupplierDto>(supplierModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SupplierService.CreateSupplier(supplierDto, createdBy);

                var returnedId = supplierDto?.id ?? 0;

                var createdModel = _mapper.Map<SupplierModel>(supplierDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating supplier", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/supplier/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierModel supplierModel)
        {
            if (supplierModel == null || id != supplierModel.id)
                return BadRequest("Invalid supplier data.");

            try
            {
                var supplierDto = _mapper.Map<SupplierDto>(supplierModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SupplierService.UpdateSupplier(supplierDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating supplier with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/supplier/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var supplier = await _serviceManager.SupplierService.GetById(id);
                if (supplier == null)
                    return NotFound();

                // Kung meron kang Delete method sa ISupplierService, call it here.
                // await _SupplierService.Deletesupplier(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting supplier with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/supplier/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<SupplierModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var suppliers = await _serviceManager.SupplierService.SearchSuppliersByKeywordAsync(keyword);
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching suppliers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
