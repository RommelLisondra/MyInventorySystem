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
    public class ReceivingReportController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ReceivingReportController));

        public ReceivingReportController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/receivingReport
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReceivingReportMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var receivingReports = await _serviceManager.ReceivingReportService.GetAll();
                return Ok(receivingReports);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching receivingReports", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ReceivingReportMasterModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var receivingReports = await _serviceManager.ReceivingReportService.GetAllPaged(pageNumber, pageSize);
                return Ok(receivingReports);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged receivingReports", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/receivingReport/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReceivingReportMasterModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var receivingReport = await _serviceManager.ReceivingReportService.GetById(id);
                if (receivingReport == null)
                    return NotFound();

                return Ok(receivingReport);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching receivingReport by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/receivingReport
        [HttpPost]
        [ProducesResponseType(typeof(ReceivingReportMasterModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ReceivingReportMasterModel receivingReportMasterModel)
        {
            if (receivingReportMasterModel == null)
                return BadRequest("receivingReport data cannot be null.");

            try
            {
                var receivingReportDto = _mapper.Map<ReceivingReportMasterDto>(receivingReportMasterModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ReceivingReportService.CreateReceivingReportAsync(receivingReportDto, createdBy);

                var returnedId = receivingReportDto?.id ?? 0;

                var createdModel = _mapper.Map<ReceivingReportMasterModel>(receivingReportDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating receivingReport", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/receivingReport/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ReceivingReportMasterModel receivingReportMasterModel)
        {
            if (receivingReportMasterModel == null || id != receivingReportMasterModel.id)
                return BadRequest("Invalid receivingReport data.");

            try
            {
                var receivingReportDto = _mapper.Map<ReceivingReportMasterDto>(receivingReportMasterModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ReceivingReportService.UpdateReceivingReportAsync(receivingReportDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating receivingReport with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/receivingReport/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var receivingReport = await _serviceManager.ReceivingReportService.GetById(id);
                if (receivingReport == null)
                    return NotFound();

                // Kung meron kang Delete method sa IReceivingReportService, call it here.
                // await _ReceivingReportService.DeletereceivingReport(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting receivingReport with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/receivingReport/search?keyword=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ReceivingReportMasterModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var receivingReports = await _serviceManager.ReceivingReportService.SearchReceivingReportByKeywordAsync(keyword);
                return Ok(receivingReports);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching receivingReports", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
