using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.ApplicationService;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.WebAPI.Model;
using System.Net;

namespace Sample.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentSeriesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(DocumentSeriesController));

        public DocumentSeriesController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/DocumentSeries
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DocumentSeriesModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var documentSeries = await _serviceManager.DocumentSeriesService.GetAll();
                return Ok(documentSeries);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching DocumentSeriess", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<DocumentSeriesModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var documentSeries = await _serviceManager.DocumentSeriesService.GetAllPaged(pageNumber, pageSize);
                return Ok(documentSeries);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/DocumentSeries/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocumentSeriesModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var documentSeries = await _serviceManager.DocumentSeriesService.GetById(id);
                if (documentSeries == null)
                    return NotFound();

                return Ok(documentSeries);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching DocumentSeries by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/DocumentSeries
        [HttpPost]
        [ProducesResponseType(typeof(DocumentSeriesModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DocumentSeriesModel documentSeriesModel)
        {
            if (documentSeriesModel == null)
                return BadRequest("DocumentSeries data cannot be null.");

            try
            {
                var documentSeriesDto = _mapper.Map<DocumentSeriesDto>(documentSeriesModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.DocumentSeriesService.CreateDocumentSeries(documentSeriesDto, createdBy);

                var returnedId = documentSeriesDto?.id ?? 0;

                var createdModel = _mapper.Map<DocumentSeriesModel>(documentSeriesDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating DocumentSeries", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/DocumentSeries/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] DocumentSeriesModel documentSeriesModel)
        {
            if (documentSeriesModel == null || id != documentSeriesModel.id)
                return BadRequest("Invalid DocumentSeries data.");

            try
            {
                var documentSeriesDto = _mapper.Map<DocumentSeriesDto>(documentSeriesModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.DocumentSeriesService.UpdateDocumentSeries(documentSeriesDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating DocumentSeries with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/DocumentSeries/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var documentSeries = await _serviceManager.DocumentSeriesService.GetById(id);
                if (documentSeries == null)
                    return NotFound();

                // Kung meron kang Delete method sa IDocumentSeriesService, call it here.
                // await _DocumentSeriesService.DeleteDocumentSeries(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting DocumentSeries with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/DocumentSeries/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<DocumentSeriesModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var documentSeries = await _serviceManager.DocumentSeriesService.SearchDocumentSeriessByKeywordAsync(keyword);
                return Ok(documentSeries);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching DocumentSeriess", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
