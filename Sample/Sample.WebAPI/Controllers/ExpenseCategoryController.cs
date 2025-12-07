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
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ExpenseCategoryController));

        public ExpenseCategoryController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/ExpenseCategory
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExpenseCategoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var expenseCategories = await _serviceManager.ExpenseCategoryService.GetAll();
                return Ok(expenseCategories);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ExpenseCategoryModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var expenseCategories = await _serviceManager.ExpenseCategoryService.GetAllPaged(pageNumber, pageSize);
                return Ok(expenseCategories);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ExpenseCategory/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ExpenseCategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var expenseCategory = await _serviceManager.ExpenseCategoryService.GetById(id);
                if (expenseCategory == null)
                    return NotFound();

                return Ok(expenseCategory);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval Flow by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/ExpenseCategory
        [HttpPost]
        [ProducesResponseType(typeof(ExpenseCategoryModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ExpenseCategoryModel expenseCategoryModel)
        {
            if (expenseCategoryModel == null)
                return BadRequest("Approval Flow data cannot be null.");

            try
            {
                var expenseCategoryDto = _mapper.Map<ExpenseCategoryDto>(expenseCategoryModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ExpenseCategoryService.CreateExpenseCategory(expenseCategoryDto, createdBy);

                var returnedId = expenseCategoryDto?.id ?? 0;

                var createdModel = _mapper.Map<ExpenseCategoryModel>(expenseCategoryDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating ExpenseCategory", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/ExpenseCategory/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseCategoryModel expenseCategoryModel)
        {
            if (expenseCategoryModel == null || id != expenseCategoryModel.id)
                return BadRequest("Invalid ExpenseCategory data.");

            try
            {
                var expenseCategoryDto = _mapper.Map<ExpenseCategoryDto>(expenseCategoryModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ExpenseCategoryService.UpdateExpenseCategory(expenseCategoryDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/ExpenseCategory/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var expenseCategory = await _serviceManager.ExpenseCategoryService.GetById(id);
                if (expenseCategory == null)
                    return NotFound();

                // Kung meron kang Delete method sa IExpenseCategoryService, call it here.
                // await _ExpenseCategoryService.DeleteExpenseCategory(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting ExpenseCategory with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/ExpenseCategory/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ExpenseCategoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var expenseCategories = await _serviceManager.ExpenseCategoryService.SearchExpenseCategorysByKeywordAsync(keyword);
                return Ok(expenseCategories);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
