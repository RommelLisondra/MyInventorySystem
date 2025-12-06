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
    public class OfficialReceiptController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(OfficialReceiptController));

        public OfficialReceiptController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/officialReceipt
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OfficialReceiptMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var officialReceipts = await _serviceManager.OfficialReceiptService.GetAll();
                return Ok(officialReceipts);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching officialReceipts", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<OfficialReceiptMasterModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var officialReceipts = await _serviceManager.OfficialReceiptService.GetAllPaged(pageNumber, pageSize);
                return Ok(officialReceipts);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged officialReceipts", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/officialReceipt/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OfficialReceiptMasterModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var officialReceipt = await _serviceManager.OfficialReceiptService.GetById(id);
                if (officialReceipt == null)
                    return NotFound();

                return Ok(officialReceipt);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching officialReceipt by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/officialReceipt
        [HttpPost]
        [ProducesResponseType(typeof(OfficialReceiptMasterModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] OfficialReceiptMasterModel officialReceiptMasterModel)
        {
            if (officialReceiptMasterModel == null)
                return BadRequest("officialReceipt data cannot be null.");

            try
            {
                var officialReceiptDto = _mapper.Map<OfficialReceiptMasterDto>(officialReceiptMasterModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.OfficialReceiptService.CreateOfficialReceiptAsync(officialReceiptDto, createdBy);

                var returnedId = officialReceiptDto?.id ?? 0;

                var createdModel = _mapper.Map<OfficialReceiptMasterModel>(officialReceiptDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating officialReceipt", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/officialReceipt/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] OfficialReceiptMasterModel officialReceiptMasterModel)
        {
            if (officialReceiptMasterModel == null || id != officialReceiptMasterModel.id)
                return BadRequest("Invalid officialReceipt data.");

            try
            {
                var officialReceiptDto = _mapper.Map<OfficialReceiptMasterDto>(officialReceiptMasterModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.OfficialReceiptService.UpdateOfficialReceiptAsync(officialReceiptDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating officialReceipt with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/officialReceipt/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var officialReceipt = await _serviceManager.OfficialReceiptService.GetById(id);
                if (officialReceipt == null)
                    return NotFound();

                // Kung meron kang Delete method sa IOfficialReceiptService, call it here.
                // await _OfficialReceiptService.DeleteofficialReceipt(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting officialReceipt with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/officialReceipt/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<OfficialReceiptMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var officialReceipts = await _serviceManager.OfficialReceiptService.SearchOfficialReceiptByKeywordAsync(keyword);
                return Ok(officialReceipts);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching officialReceipts", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
