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
    public class SalesOrderController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SalesOrderController));

        public SalesOrderController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/saleorder
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SalesOrderMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var saleorders = await _serviceManager.SalesOrderService.GetAll();
                return Ok(saleorders);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching saleorders", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<SalesOrderMasterModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var saleorders = await _serviceManager.SalesOrderService.GetAllPaged(pageNumber, pageSize);
                return Ok(saleorders);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged saleorders", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/saleorder/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SalesOrderMasterModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var saleorder = await _serviceManager.SalesOrderService.GetById(id);
                if (saleorder == null)
                    return NotFound();

                return Ok(saleorder);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching saleorder by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/saleorder
        [HttpPost]
        [ProducesResponseType(typeof(SalesOrderMasterModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SalesOrderMasterModel salesOrderMasterModel)
        {
            if (salesOrderMasterModel == null)
                return BadRequest("saleorder data cannot be null.");

            try
            {
                var saleorderDto = _mapper.Map<SalesOrderMasterDto>(salesOrderMasterModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SalesOrderService.CreateSalesOrderAsync(saleorderDto, createdBy);

                var returnedId = saleorderDto?.id ?? 0;

                var createdModel = _mapper.Map<SalesOrderMasterModel>(saleorderDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating saleorder", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/saleorder/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] SalesOrderMasterModel salesOrderMasterModel)
        {
            if (salesOrderMasterModel == null || id != salesOrderMasterModel.id)
                return BadRequest("Invalid saleorder data.");

            try
            {
                var saleorderDto = _mapper.Map<SalesOrderMasterDto>(salesOrderMasterModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SalesOrderService.UpdateSalesOrderAsync(saleorderDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating saleorder with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/saleorder/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var saleorder = await _serviceManager.SalesOrderService.GetById(id);
                if (saleorder == null)
                    return NotFound();

                // Kung meron kang Delete method sa ISalesOrderService, call it here.
                // await _SalesOrderService.Deletesaleorder(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting saleorder with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/saleorder/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<SalesOrderMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var saleorders = await _serviceManager.SalesOrderService.SearchSalesOrderByKeywordAsync(keyword);
                return Ok(saleorders);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching saleorders", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
