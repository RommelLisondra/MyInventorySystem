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
    public class RolePermissionController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(RolePermissionController));

        public RolePermissionController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/RolePermission
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RolePermissionModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var rolePermissions = await _serviceManager.RolePermissionService.GetAll();
                return Ok(rolePermissions);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching rolePermissions", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<RolePermissionModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var rolePermissions = await _serviceManager.RolePermissionService.GetAllPaged(pageNumber, pageSize);
                return Ok(rolePermissions);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged rolePermissions", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/RolePermission/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RolePermissionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var rolePermission = await _serviceManager.RolePermissionService.GetById(id);
                if (rolePermission == null)
                    return NotFound();

                return Ok(rolePermission);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching rolePermission by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/RolePermission
        [HttpPost]
        [ProducesResponseType(typeof(RolePermissionModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] RolePermissionModel rolePermissionModel)
        {
            if (rolePermissionModel == null)
                return BadRequest("RolePermission data cannot be null.");

            try
            {
                var rolePermissionDto = _mapper.Map<RolePermissionDto>(rolePermissionModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.RolePermissionService.CreateRolePermission(rolePermissionDto, createdBy);

                var returnedId = rolePermissionDto?.id ?? 0;

                var createdModel = _mapper.Map<RolePermissionModel>(rolePermissionDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating RolePermission", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/RolePermission/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] RolePermissionModel rolePermissionModel)
        {
            if (rolePermissionModel == null || id != rolePermissionModel.id)
                return BadRequest("Invalid RolePermission data.");

            try
            {
                var rolePermissionDto = _mapper.Map<RolePermissionDto>(rolePermissionModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.RolePermissionService.UpdateRolePermission(rolePermissionDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating RolePermission with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/RolePermission/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var rolePermission = await _serviceManager.RolePermissionService.GetById(id);
                if (rolePermission == null)
                    return NotFound();

                // Kung meron kang Delete method sa IRolePermissionService, call it here.
                // await _RolePermissionService.DeleteRolePermission(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting RolePermission with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/RolePermission/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<RolePermissionModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var rolePermission = await _serviceManager.RolePermissionService.SearchRolePermissionsByKeywordAsync(keyword);
                return Ok(rolePermission);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching rolePermission", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
