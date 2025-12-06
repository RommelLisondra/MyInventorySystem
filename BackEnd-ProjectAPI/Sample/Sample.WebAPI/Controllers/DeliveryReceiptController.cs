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
    public class DeliveryReceiptController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(DeliveryReceiptController));

        public DeliveryReceiptController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/delivery
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeliveryReceiptMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var deliveries = await _serviceManager.DeliveryReceiptService.GetAll();
                return Ok(deliveries);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching deliveries", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<DeliveryReceiptMasterModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var deliveries = await _serviceManager.DeliveryReceiptService.GetAllPaged(pageNumber, pageSize);
                return Ok(deliveries);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/delivery/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeliveryReceiptMasterModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var delivery = await _serviceManager.DeliveryReceiptService.GetById(id);
                if (delivery == null)
                    return NotFound();

                return Ok(delivery);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching delivery by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/delivery
        [HttpPost]
        [ProducesResponseType(typeof(DeliveryReceiptMasterModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DeliveryReceiptMasterModel deliveryReceiptMasterModel)
        {
            if (deliveryReceiptMasterModel == null)
                return BadRequest("delivery data cannot be null.");

            try
            {
                var deliveryDto = _mapper.Map<DeliveryReceiptMasterDto>(deliveryReceiptMasterModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.DeliveryReceiptService.CreateDeliveryReceiptAsync(deliveryDto, createdBy);

                var returnedId = deliveryDto?.id ?? 0;

                var createdModel = _mapper.Map<DeliveryReceiptMasterModel>(deliveryDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating delivery", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/delivery/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] DeliveryReceiptMasterModel deliveryReceiptMasterModel)
        {
            if (deliveryReceiptMasterModel == null || id != deliveryReceiptMasterModel.id)
                return BadRequest("Invalid delivery data.");

            try
            {
                var deliveryDto = _mapper.Map<DeliveryReceiptMasterDto>(deliveryReceiptMasterModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.DeliveryReceiptService.UpdateDeliveryReceiptAsync(deliveryDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating delivery with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/delivery/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var delivery = await _serviceManager.DeliveryReceiptService.GetById(id);
                if (delivery == null)
                    return NotFound();

                // Kung meron kang Delete method sa IDeliveryReceiptService, call it here.
                // await _DeliveryReceiptService.Deletedelivery(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting delivery with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/delivery/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<DeliveryReceiptMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var deliveries = await _serviceManager.DeliveryReceiptService.SearchDeliveryReceiptByKeywordAsync(keyword);
                return Ok(deliveries);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching deliveries", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
