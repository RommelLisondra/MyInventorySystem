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
    public class PurchaseReturnController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(PurchaseReturnController));

        public PurchaseReturnController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/purchaseReturn
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PurchaseReturnMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var purchaseReturns = await _serviceManager.PurchaseReturnService.GetAll();
                return Ok(purchaseReturns);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching purchaseReturns", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<PurchaseReturnMasterModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var purchaseReturns = await _serviceManager.PurchaseReturnService.GetAllPaged(pageNumber, pageSize);
                return Ok(purchaseReturns);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged purchaseReturns", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/purchaseReturn/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PurchaseReturnMasterModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var purchaseReturn = await _serviceManager.PurchaseReturnService.GetById(id);
                if (purchaseReturn == null)
                    return NotFound();

                return Ok(purchaseReturn);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching purchaseReturn by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/purchaseReturn
        [HttpPost]
        [ProducesResponseType(typeof(PurchaseReturnMasterModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] PurchaseReturnMasterModel purchaseReturnMasterModel)
        {
            if (purchaseReturnMasterModel == null)
                return BadRequest("purchaseReturn data cannot be null.");

            try
            {
                var purchaseReturnDto = _mapper.Map<PurchaseReturnMasterDto>(purchaseReturnMasterModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.PurchaseReturnService.CreatePurchaseReturnAsync(purchaseReturnDto, createdBy);

                var returnedId = purchaseReturnDto?.id ?? 0;

                var createdModel = _mapper.Map<PurchaseReturnMasterModel>(purchaseReturnDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating purchaseReturn", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/purchaseReturn/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] PurchaseReturnMasterModel purchaseReturnMasterModel)
        {
            if (purchaseReturnMasterModel == null || id != purchaseReturnMasterModel.id)
                return BadRequest("Invalid purchaseReturn data.");

            try
            {
                var purchaseReturnDto = _mapper.Map<PurchaseReturnMasterDto>(purchaseReturnMasterModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.PurchaseReturnService.UpdatePurchaseRetunrAsync(purchaseReturnDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating purchaseReturn with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/purchaseReturn/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var purchaseReturn = await _serviceManager.PurchaseReturnService.GetById(id);
                if (purchaseReturn == null)
                    return NotFound();

                // Kung meron kang Delete method sa IPurchaseReturnService, call it here.
                // await _PurchaseReturnService.DeletepurchaseReturn(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting purchaseReturn with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/purchaseReturn/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<PurchaseReturnMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var purchaseReturns = await _serviceManager.PurchaseReturnService.SearchPurchaseReturnByKeywordAsync(keyword);
                return Ok(purchaseReturns);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching purchaseReturns", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
