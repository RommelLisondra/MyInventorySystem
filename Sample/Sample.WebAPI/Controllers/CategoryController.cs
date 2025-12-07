using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.ApplicationService;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.WebAPI.Model;
using System.Net;

namespace Sample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(CategoryController));

        public CategoryController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/Category
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _serviceManager.CategoryService.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<CategoryModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var categories = await _serviceManager.CategoryService.GetAllPaged(pageNumber, pageSize);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Category/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _serviceManager.CategoryService.GetById(id);
                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval Flow by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/Category
        [HttpPost]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CategoryModel categoryModel)
        {
            if (categoryModel == null)
                return BadRequest("Approval Flow data cannot be null.");

            try
            {
                var categoryDto = _mapper.Map<CategoryDto>(categoryModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.CategoryService.CreateCategory(categoryDto, createdBy);

                var returnedId = categoryDto?.id ?? 0;

                var createdModel = _mapper.Map<CategoryModel>(categoryDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating Category", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/Category/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryModel categoryModel)
        {
            if (categoryModel == null || id != categoryModel.id)
                return BadRequest("Invalid Category data.");

            try
            {
                var categoryDto = _mapper.Map<CategoryDto>(categoryModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.CategoryService.UpdateCategory(categoryDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/Category/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _serviceManager.CategoryService.GetById(id);
                if (category == null)
                    return NotFound();

                // Kung meron kang Delete method sa ICategoryService, call it here.
                // await _CategoryService.DeleteCategory(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting Category with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Category/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<CategoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var categories = await _serviceManager.CategoryService.SearchCategorysByKeywordAsync(keyword);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
