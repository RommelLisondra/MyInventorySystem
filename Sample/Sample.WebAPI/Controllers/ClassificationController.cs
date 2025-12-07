using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.ApplicationService;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.WebAPI.Model;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ClassificationController));

        public ClassificationController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/Classification
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClassificationModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var classifications = await _serviceManager.ClassificationService.GetAll();
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ClassificationModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var classifications = await _serviceManager.ClassificationService.GetAllPaged(pageNumber, pageSize);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Classification/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClassificationModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var classifications = await _serviceManager.ClassificationService.GetById(id);
                if (classifications == null)
                    return NotFound();

                return Ok(classifications);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval Flow by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/Classification
        [HttpPost]
        [ProducesResponseType(typeof(ClassificationModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ClassificationModel classificationModel)
        {
            if (classificationModel == null)
                return BadRequest("Approval Flow data cannot be null.");

            try
            {
                var classificationDto = _mapper.Map<ClassificationDto>(classificationModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ClassificationService.CreateClassification(classificationDto, createdBy);

                var returnedId = classificationDto?.id ?? 0;

                var createdModel = _mapper.Map<ClassificationModel>(classificationDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating Classification", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/Classification/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ClassificationModel classificationModel)
        {
            if (classificationModel == null || id != classificationModel.id)
                return BadRequest("Invalid Classification data.");

            try
            {
                var classificationDto = _mapper.Map<ClassificationDto>(classificationModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ClassificationService.UpdateClassification(classificationDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/Classification/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var classification = await _serviceManager.ClassificationService.GetById(id);
                if (classification == null)
                    return NotFound();

                // Kung meron kang Delete method sa IClassificationService, call it here.
                // await _ClassificationService.DeleteClassification(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting Classification with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Classification/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ClassificationModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var classifications = await _serviceManager.ClassificationService.SearchClassificationsByKeywordAsync(keyword);
                return Ok(classifications);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
