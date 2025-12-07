using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Sample.ApplicationService;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Domain.Entities;
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
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(PurchaseOrderController));

        public PurchaseOrderController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/purchaseOrder
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PurchaseOrderMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var purchaseOrders = await _serviceManager.PurchaseOrderService.GetAll();
                return Ok(purchaseOrders);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching purchaseOrders", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<PurchaseOrderMasterModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var purchaseOrders = await _serviceManager.PurchaseOrderService.GetAllPaged(pageNumber, pageSize);
                return Ok(purchaseOrders);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged purchaseOrders", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/purchaseOrder/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PurchaseOrderMasterModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var purchaseOrder = await _serviceManager.PurchaseOrderService.GetById(id);
                if (purchaseOrder == null)
                    return NotFound();

                return Ok(purchaseOrder);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching purchaseOrder by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/purchaseOrder
        [HttpPost]
        [ProducesResponseType(typeof(PurchaseOrderMasterModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] PurchaseOrderMasterModel purchaseOrderMasterModel)
        {
            if (purchaseOrderMasterModel == null)
                return BadRequest("purchaseOrder data cannot be null.");

            try
            {
                var purchaseOrderDto = _mapper.Map<PurchaseOrderMasterDto>(purchaseOrderMasterModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.PurchaseOrderService.CreatePurchaseOrderAsync(purchaseOrderDto, createdBy);

                var returnedId = purchaseOrderDto?.id ?? 0;

                var createdModel = _mapper.Map<PurchaseOrderMasterModel>(purchaseOrderDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating purchaseOrder", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/purchaseOrder/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] PurchaseOrderMasterModel purchaseOrderMasterModel)
        {
            if (purchaseOrderMasterModel == null || id != purchaseOrderMasterModel.id)
                return BadRequest("Invalid purchaseOrder data.");

            try
            {
                var purchaseOrderDto = _mapper.Map<PurchaseOrderMasterDto>(purchaseOrderMasterModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.PurchaseOrderService.UpdatePurchaseOrderAsync(purchaseOrderDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating purchaseOrder with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/purchaseOrder/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var purchaseOrder = await _serviceManager.PurchaseOrderService.GetById(id);
                if (purchaseOrder == null)
                    return NotFound();

                // Kung meron kang Delete method sa IPurchaseOrderService, call it here.
                // await _PurchaseOrderService.DeletepurchaseOrder(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting purchaseOrder with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/purchaseOrder/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<PurchaseOrderMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var purchaseOrders = await _serviceManager.PurchaseOrderService.SearchPurchaseOrderByKeywordAsync(keyword);
                return Ok(purchaseOrders);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching purchaseOrders", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
