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
    public class SalesInvoiceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SalesInvoiceController));

        public SalesInvoiceController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/invoice
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SalesInvoiceMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var invoices = await _serviceManager.SalesInvoiceService.GetAll();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching invoices", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<SalesInvoiceMasterModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var invoices = await _serviceManager.SalesInvoiceService.GetAllPaged(pageNumber, pageSize);
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged invoices", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/invoice/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SalesInvoiceMasterModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var invoice = await _serviceManager.SalesInvoiceService.GetById(id);
                if (invoice == null)
                    return NotFound();

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching invoice by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/invoice
        [HttpPost]
        [ProducesResponseType(typeof(SalesInvoiceMasterModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SalesInvoiceMasterModel salesInvoiceMasterModel)
        {
            if (salesInvoiceMasterModel == null)
                return BadRequest("invoice data cannot be null.");

            try
            {
                var invoiceDto = _mapper.Map<SalesInvoiceMasterDto>(salesInvoiceMasterModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SalesInvoiceService.CreateSalesInvoiceAsync(invoiceDto, createdBy);

                var returnedId = invoiceDto?.id ?? 0;

                var createdModel = _mapper.Map<SalesInvoiceMasterModel>(invoiceDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating invoice", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/invoice/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] SalesInvoiceMasterModel salesInvoiceMasterModel)
        {
            if (salesInvoiceMasterModel == null || id != salesInvoiceMasterModel.id)
                return BadRequest("Invalid invoice data.");

            try
            {
                var invoiceDto = _mapper.Map<SalesInvoiceMasterDto>(salesInvoiceMasterModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SalesInvoiceService.UpdateSalesinvoiceAsync(invoiceDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating invoice with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/invoice/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var invoice = await _serviceManager.SalesInvoiceService.GetById(id);
                if (invoice == null)
                    return NotFound();

                // Kung meron kang Delete method sa ISalesInvoiceService, call it here.
                // await _SalesInvoiceService.Deleteinvoice(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting invoice with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/invoice/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<SalesInvoiceMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var invoices = await _serviceManager.SalesInvoiceService.SearchSalesInvoiceByKeywordAsync(keyword);
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching invoices", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
