using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Sample.ApplicationService;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.WebAPI;
using Sample.WebAPI.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Sample.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(LocationController));

        public LocationController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/location
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocationModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var locations = await _serviceManager.LocationService.GetAll();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching locations", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<LocationModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var locations = await _serviceManager.LocationService.GetAllPaged(pageNumber, pageSize);
                return Ok(locations);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged locations", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/location/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var location = await _serviceManager.LocationService.GetById(id);
                if (location == null)
                    return NotFound();

                return Ok(location);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching location by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/location
        [HttpPost]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] LocationModel locationModel)
        {
            if (locationModel == null)
                return BadRequest("location data cannot be null.");

            try
            {
                var locationDto = _mapper.Map<LocationDto>(locationModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.LocationService.CreateLocation(locationDto, createdBy);

                var returnedId = locationDto?.id ?? 0;

                var createdModel = _mapper.Map<LocationModel>(locationDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating location", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/location/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] LocationModel locationModel)
        {
            if (locationModel == null || id != locationModel.id)
                return BadRequest("Invalid location data.");

            try
            {
                var locationDto = _mapper.Map<LocationDto>(locationModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.LocationService.UpdateLocation(locationDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating location with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/location/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var location = await _serviceManager.LocationService.GetById(id);
                if (location == null)
                    return NotFound();

                // Kung meron kang Delete method sa ILocationService, call it here.
                // await _LocationService.Deletelocation(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting location with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/location/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<LocationModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? name)
        {
            try
            {
                var locations = await _serviceManager.LocationService.SearchLocationsAsync(name);
                return Ok(locations);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching locations", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
