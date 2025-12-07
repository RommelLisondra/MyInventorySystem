using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.ApplicationService;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.WebAPI.Auth;
using Sample.WebAPI.Model;
using System.Net;

namespace Sample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAcountController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(UserAcountController));
        private readonly JwtTokenGenerator _token;

        public UserAcountController(IServiceManager serviceManager, IMapper mapper, JwtTokenGenerator token)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _token = token;
        }

        // GET api/UserAcount
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserAccountModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userAcounts = await _serviceManager.UserAcountService.GetAll();
                return Ok(userAcounts);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching UserAccounts", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<UserAccountModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var userAcounts = await _serviceManager.UserAcountService.GetAllPaged(pageNumber, pageSize);
                return Ok(userAcounts);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged userAcounts", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST /login
        [HttpPost("login")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserAccountModel loginDto)
        {
            try
            {
                var userAccount = await _serviceManager.UserAcountService
                    .GetLoginUserAccount(loginDto.Username, loginDto.PasswordHash);

                if (userAccount == null)
                    return Unauthorized("Invalid username or password");

                string token = _token.GenerateToken(
                    userId: userAccount.id.ToString(),
                    username: userAccount.Username!,
                    role: userAccount.Role!.RoleName!
                );

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                logCentral.Error("Error during login", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/UserAcount/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserAccountModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var userAcount = await _serviceManager.UserAcountService.GetById(id);
                if (userAcount == null)
                    return NotFound();

                return Ok(userAcount);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching UserAccount by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/UserAcount
        [HttpPost]
        [ProducesResponseType(typeof(UserAccountModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] UserAccountModel userAccountModel)
        {
            if (userAccountModel == null)
                return BadRequest("UserAcount data cannot be null.");

            try
            {
                var userAccountDto = _mapper.Map<UserAccountDto>(userAccountModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.UserAcountService.CreateUserAccount(userAccountDto, createdBy);

                var returnedId = userAccountDto?.id ?? 0;

                var createdModel = _mapper.Map<UserAccountModel>(userAccountDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating UserAccount", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/UserAcount/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UserAccountModel userAccountModel)
        {
            if (userAccountModel == null || id != userAccountModel.id)
                return BadRequest("Invalid UserAccount data.");

            try
            {
                var userAccountDto = _mapper.Map<UserAccountDto>(userAccountModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.UserAcountService.UpdateUserAccount(userAccountDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating UserAccount with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/UserAcount/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userAccount = await _serviceManager.UserAcountService.GetById(id);
                if (userAccount == null)
                    return NotFound();

                // Kung meron kang Delete method sa IUserAcountService, call it here.
                // await _UserAcountService.DeleteUserAcount(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting UserAccount with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/UserAcount/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<UserAccountModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var userAcounts = await _serviceManager.UserAcountService.SearchUserAccountsByKeywordAsync(keyword);
                return Ok(userAcounts);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching UserAccounts", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
