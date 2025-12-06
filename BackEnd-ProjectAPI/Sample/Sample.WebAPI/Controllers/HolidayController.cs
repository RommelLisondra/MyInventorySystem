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
    public class HolidayController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(HolidayController));

        public HolidayController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/Holiday
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HolidayModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var holidays = await _serviceManager.HolidayService.GetAll();
                return Ok(holidays);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<HolidayModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var holidays = await _serviceManager.HolidayService.GetAllPaged(pageNumber, pageSize);
                return Ok(holidays);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Holiday/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(HolidayModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var holiday = await _serviceManager.HolidayService.GetById(id);
                if (holiday == null)
                    return NotFound();

                return Ok(holiday);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching holiday by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/Holiday
        [HttpPost]
        [ProducesResponseType(typeof(HolidayModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] HolidayModel holidayModel)
        {
            if (holidayModel == null)
                return BadRequest("holiday data cannot be null.");

            try
            {
                var HolidayDto = _mapper.Map<HolidayDto>(holidayModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.HolidayService.CreateHoliday(HolidayDto, createdBy);

                var returnedId = HolidayDto?.id ?? 0;

                var createdModel = _mapper.Map<HolidayModel>(HolidayDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating Holiday", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/Holiday/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] HolidayModel holidayModel)
        {
            if (holidayModel == null || id != holidayModel.id)
                return BadRequest("Invalid Holiday data.");

            try
            {
                var holidayDto = _mapper.Map<HolidayDto>(holidayModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.HolidayService.UpdateHoliday(holidayDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating Approval Flow with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/Holiday/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var holiday = await _serviceManager.HolidayService.GetById(id);
                if (holiday == null)
                    return NotFound();

                // Kung meron kang Delete method sa IHolidayService, call it here.
                // await _HolidayService.DeleteHoliday(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting Holiday with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/Holiday/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<HolidayModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var holiday = await _serviceManager.HolidayService.SearchHolidaysByKeywordAsync(keyword);
                return Ok(holiday);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching Approval Flows", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
