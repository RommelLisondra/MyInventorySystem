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
    public class ExpenseController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ExpenseController));

        public ExpenseController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/Expense
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExpenseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var expenses = await _serviceManager.ExpenseService.GetAll();
                return Ok(expenses);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ExpenseModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var expenses = await _serviceManager.ExpenseService.GetAllPaged(pageNumber, pageSize);
                return Ok(expenses);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Expense/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ExpenseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var expense = await _serviceManager.ExpenseService.GetById(id);
                if (expense == null)
                    return NotFound();

                return Ok(expense);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval Flow by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/Expense
        [HttpPost]
        [ProducesResponseType(typeof(ExpenseModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ExpenseModel expenseModel)
        {
            if (expenseModel == null)
                return BadRequest("Approval Flow data cannot be null.");

            try
            {
                var expenseDto = _mapper.Map<ExpenseDto>(expenseModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ExpenseService.CreateExpense(expenseDto, createdBy);

                var returnedId = expenseDto?.id ?? 0;

                var createdModel = _mapper.Map<ExpenseModel>(expenseDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating Expense", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/Expense/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseModel expenseModel)
        {
            if (expenseModel == null || id != expenseModel.id)
                return BadRequest("Invalid Expense data.");

            try
            {
                var expenseDto = _mapper.Map<ExpenseDto>(expenseModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.ExpenseService.UpdateExpense(expenseDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/Expense/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var expense = await _serviceManager.ExpenseService.GetById(id);
                if (expense == null)
                    return NotFound();

                // Kung meron kang Delete method sa IExpenseService, call it here.
                // await _ExpenseService.DeleteExpense(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting Expense with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Expense/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ExpenseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var expenses = await _serviceManager.ExpenseService.SearchExpensesByKeywordAsync(keyword);
                return Ok(expenses);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
