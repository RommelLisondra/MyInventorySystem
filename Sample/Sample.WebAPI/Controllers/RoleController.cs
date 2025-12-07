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
    public class RoleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(RoleController));

        public RoleController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/Role
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var roles = await _serviceManager.RoleService.GetAll();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Roles", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<RoleModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var roles = await _serviceManager.RoleService.GetAllPaged(pageNumber, pageSize);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged roles", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Role/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var role = await _serviceManager.RoleService.GetById(id);
                if (role == null)
                    return NotFound();

                return Ok(role);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching Role by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/Role
        [HttpPost]
        [ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] RoleModel roleModel)
        {
            if (roleModel == null)
                return BadRequest("Role data cannot be null.");

            try
            {
                var roleDto = _mapper.Map<RoleDto>(roleModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.RoleService.CreateRole(roleDto, createdBy);

                var returnedId = roleDto?.id ?? 0;

                var createdModel = _mapper.Map<RoleModel>(roleDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating Role", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/Role/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] RoleModel roleModel)
        {
            if (roleModel == null || id != roleModel.id)
                return BadRequest("Invalid Role data.");

            try
            {
                var roleDto = _mapper.Map<RoleDto>(roleModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.RoleService.UpdateRole(roleDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Role with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/Role/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var role = await _serviceManager.RoleService.GetById(id);
                if (role == null)
                    return NotFound();

                // Kung meron kang Delete method sa IRoleService, call it here.
                // await _RoleService.DeleteRole(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting Role with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Role/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<RoleModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var roles = await _serviceManager.RoleService.SearchRolesByKeywordAsync(keyword);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Roles", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
