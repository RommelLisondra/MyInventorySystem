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
    public class SubCategoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SubCategoryController));

        public SubCategoryController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/SubCategory
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubCategoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var subCategories = await _serviceManager.SubCategoryService.GetAll();
                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<SubCategoryModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var subCategories = await _serviceManager.SubCategoryService.GetAllPaged(pageNumber, pageSize);
                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/SubCategory/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SubCategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var subCategory = await _serviceManager.SubCategoryService.GetById(id);
                if (subCategory == null)
                    return NotFound();

                return Ok(subCategory);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval Flow by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/SubCategory
        [HttpPost]
        [ProducesResponseType(typeof(SubCategoryModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SubCategoryModel subCategoryModel)
        {
            if (subCategoryModel == null)
                return BadRequest("Approval Flow data cannot be null.");

            try
            {
                var subCategoryDto = _mapper.Map<SubCategoryDto>(subCategoryModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SubCategoryService.CreateSubCategory(subCategoryDto, createdBy);

                var returnedId = subCategoryDto?.id ?? 0;

                var createdModel = _mapper.Map<SubCategoryModel>(subCategoryDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating SubCategory", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/SubCategory/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] SubCategoryModel subCategoryModel)
        {
            if (subCategoryModel == null || id != subCategoryModel.id)
                return BadRequest("Invalid SubCategory data.");

            try
            {
                var subCategoryDto = _mapper.Map<SubCategoryDto>(subCategoryModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.SubCategoryService.UpdateSubCategory(subCategoryDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/SubCategory/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var subCategory = await _serviceManager.SubCategoryService.GetById(id);
                if (subCategory == null)
                    return NotFound();

                // Kung meron kang Delete method sa ISubCategoryService, call it here.
                // await _SubCategoryService.DeleteSubCategory(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting SubCategory with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/SubCategory/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<SubCategoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var subCategories = await _serviceManager.SubCategoryService.SearchSubCategorysByKeywordAsync(keyword);
                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
