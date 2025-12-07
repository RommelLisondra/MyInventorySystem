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
    public class AccountController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(AccountController));

        public AccountController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/Account
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var accounts = await _serviceManager.AccountService.GetAll();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<AccountModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var pagedaccounts = await _serviceManager.AccountService.GetAllPaged(pageNumber, pageSize);
                return Ok(pagedaccounts);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Account/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var account = await _serviceManager.AccountService.GetById(id);
                if (account == null)
                    return NotFound();

                return Ok(account);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Approval Flow by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/Account
        [HttpPost]
        [ProducesResponseType(typeof(AccountModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AccountModel accountModel)
        {
            if (accountModel == null)
                return BadRequest("Approval Flow data cannot be null.");

            try
            {
                var accountDto = _mapper.Map<AccountDto>(accountModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.AccountService.CreateAccount(accountDto, createdBy);

                var returnedId = accountDto?.id ?? 0;

                var createdModel = _mapper.Map<AccountModel>(accountDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating Account", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/Account/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] AccountModel accountModel)
        {
            if (accountModel == null || id != accountModel.id)
                return BadRequest("Invalid Account data.");

            try
            {
                var accountDto = _mapper.Map<AccountDto>(accountModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.AccountService.UpdateAccount(accountDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/Account/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var account = await _serviceManager.AccountService.GetById(id);
                if (account == null)
                    return NotFound();

                // Kung meron kang Delete method sa IAccountService, call it here.
                // await _AccountService.DeleteAccount(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting Account with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Account/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<AccountModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var accounts = await _serviceManager.AccountService.SearchAccountsByKeywordAsync(keyword);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
