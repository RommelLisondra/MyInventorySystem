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
    public class SalesReturnController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SalesReturnController));

        public SalesReturnController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/salesReturn
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SalesReturnMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var salesReturns = await _serviceManager.SalesReturnService.GetAll();
                return Ok(salesReturns);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching salesReturns", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<SalesReturnMasterModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var salesReturns = await _serviceManager.SalesReturnService.GetAllPaged(pageNumber, pageSize);
                return Ok(salesReturns);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/salesReturn/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SalesReturnMasterModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var salesReturn = await _serviceManager.SalesReturnService.GetById(id);
                if (salesReturn == null)
                    return NotFound();

                return Ok(salesReturn);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching salesReturn by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/salesReturn
        [HttpPost]
        [ProducesResponseType(typeof(SalesReturnMasterModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SalesReturnMasterModel salesReturnMasterModel)
        {
            if (salesReturnMasterModel == null)
                return BadRequest("salesReturn data cannot be null.");

            try
            {
                var salesReturnDto = _mapper.Map<SalesReturnMasterDto>(salesReturnMasterModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SalesReturnService.CreateSalesReturnAsync(salesReturnDto, createdBy);

                var returnedId = salesReturnDto?.id ?? 0;

                var createdModel = _mapper.Map<SalesReturnMasterModel>(salesReturnDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating salesReturn", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/salesReturn/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] SalesReturnMasterModel salesReturnMasterModel)
        {
            if (salesReturnMasterModel == null || id != salesReturnMasterModel.id)
                return BadRequest("Invalid salesReturn data.");

            try
            {
                var salesReturnDto = _mapper.Map<SalesReturnMasterDto>(salesReturnMasterModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SalesReturnService.UpdateSalesReturnAsync(salesReturnDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating salesReturn with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/salesReturn/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var salesReturn = await _serviceManager.SalesReturnService.GetById(id);
                if (salesReturn == null)
                    return NotFound();

                // Kung meron kang Delete method sa ISalesReturnService, call it here.
                // await _SalesReturnService.DeletesalesReturn(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting salesReturn with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/salesReturn/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<SalesReturnMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var salesReturns = await _serviceManager.SalesReturnService.SearchSalesReturnByKeywordAsync(keyword);
                return Ok(salesReturns);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching salesReturns", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
