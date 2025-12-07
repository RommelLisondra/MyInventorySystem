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
    public class PurchaseRequisitionController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(PurchaseRequisitionController));

        public PurchaseRequisitionController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/purchaseRequisition
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PurchaseRequisitionMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var purchaseRequisitions = await _serviceManager.PurchaseRequisitionService.GetAll();
                return Ok(purchaseRequisitions);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching purchaseRequisitions", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<PurchaseRequisitionMasterModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var purchaseRequisitions = await _serviceManager.ApprovalFlowService.GetAllPaged(pageNumber, pageSize);
                return Ok(purchaseRequisitions);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged purchaseRequisitions", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/purchaseRequisition/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PurchaseRequisitionMasterModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var purchaseRequisition = await _serviceManager.PurchaseRequisitionService.GetById(id);
                if (purchaseRequisition == null)
                    return NotFound();

                return Ok(purchaseRequisition);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching purchaseRequisition by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/purchaseRequisition
        [HttpPost]
        [ProducesResponseType(typeof(PurchaseRequisitionMasterModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] PurchaseRequisitionMasterModel purchaseRequisitionMasterModel)
        {
            if (purchaseRequisitionMasterModel == null)
                return BadRequest("purchaseRequisition data cannot be null.");

            try
            {
                var purchaseRequisitionDto = _mapper.Map<PurchaseRequisitionMasterDto>(purchaseRequisitionMasterModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.PurchaseRequisitionService.CreatePurchaseRequisitionAsync(purchaseRequisitionDto, createdBy);

                var returnedId = purchaseRequisitionDto?.id ?? 0;

                var createdModel = _mapper.Map<PurchaseRequisitionMasterModel>(purchaseRequisitionDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating purchaseRequisition", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/purchaseRequisition/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] PurchaseRequisitionMasterModel purchaseRequisitionMasterModel)
        {
            if (purchaseRequisitionMasterModel == null || id != purchaseRequisitionMasterModel.id)
                return BadRequest("Invalid purchaseRequisition data.");

            try
            {
                var purchaseRequisitionDto = _mapper.Map<PurchaseRequisitionMasterDto>(purchaseRequisitionMasterModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.PurchaseRequisitionService.UpdatePurchaseRequisitionAsync(purchaseRequisitionDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating purchaseRequisition with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/purchaseRequisition/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var purchaseRequisition = await _serviceManager.PurchaseRequisitionService.GetById(id);
                if (purchaseRequisition == null)
                    return NotFound();

                // Kung meron kang Delete method sa IPurchaseRequisitionService, call it here.
                // await _PurchaseRequisitionService.DeletepurchaseRequisition(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting purchaseRequisition with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/purchaseRequisition/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<PurchaseRequisitionMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var purchaseRequisitions = await _serviceManager.PurchaseRequisitionService.SearchPurchaseRequisitionByKeywordAsync(keyword);
                return Ok(purchaseRequisitions);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching purchaseRequisitions", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
