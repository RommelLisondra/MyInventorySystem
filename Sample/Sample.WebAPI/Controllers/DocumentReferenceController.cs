using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.ApplicationService;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.WebAPI.Model;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sample.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentReferenceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(DocumentReferenceController));

        public DocumentReferenceController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/DocumentReference
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DocumentReferenceModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var documentReferences = await _serviceManager.DocumentReferenceService.GetAll();
                return Ok(documentReferences);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching DocumentReferences", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<DocumentReferenceModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var documentReferences = await _serviceManager.DocumentReferenceService.GetAllPaged(pageNumber, pageSize);
                return Ok(documentReferences);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/DocumentReference/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocumentReferenceModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var documentReference = await _serviceManager.DocumentReferenceService.GetById(id);
                if (documentReference == null)
                    return NotFound();

                return Ok(documentReference);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching DocumentReference by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/DocumentReference
        [HttpPost]
        [ProducesResponseType(typeof(DocumentReferenceModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DocumentReferenceModel documentReferenceModel)
        {
            if (documentReferenceModel == null)
                return BadRequest("DocumentReference data cannot be null.");

            try
            {
                var documentReferenceDto = _mapper.Map<DocumentReferenceDto>(documentReferenceModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.DocumentReferenceService.CreateDocumentReference(documentReferenceDto, createdBy);

                var returnedId = documentReferenceDto?.id ?? 0;

                var createdModel = _mapper.Map<DocumentReferenceModel>(documentReferenceDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating DocumentReference", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/DocumentReference/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] DocumentReferenceModel documentReferenceModel)
        {
            if (documentReferenceModel == null || id != documentReferenceModel.id)
                return BadRequest("Invalid DocumentReference data.");

            try
            {
                var documentReferenceDto = _mapper.Map<DocumentReferenceDto>(documentReferenceModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.DocumentReferenceService.UpdateDocumentReference(documentReferenceDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating DocumentReference with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/DocumentReference/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var documentReference = await _serviceManager.DocumentReferenceService.GetById(id);
                if (documentReference == null)
                    return NotFound();

                // Kung meron kang Delete method sa IDocumentReferenceService, call it here.
                // await _DocumentReferenceService.DeleteDocumentReference(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting DocumentReference with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/DocumentReference/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<DocumentReferenceModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var documentReference = await _serviceManager.DocumentReferenceService.SearchDocumentReferencesByKeywordAsync(keyword);
                return Ok(documentReference);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Document References", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
