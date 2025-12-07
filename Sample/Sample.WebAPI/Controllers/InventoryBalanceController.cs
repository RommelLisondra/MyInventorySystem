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
    public class InventoryBalanceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(InventoryBalanceController));

        public InventoryBalanceController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/InventoryBalance
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InventoryBalanceModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var inventoryBalance = await _serviceManager.InventoryBalanceService.GetAll();
                return Ok(inventoryBalance);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching InventoryBalances", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<InventoryBalanceModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var inventoryBalance = await _serviceManager.InventoryBalanceService.GetAllPaged(pageNumber, pageSize);
                return Ok(inventoryBalance);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/InventoryBalance/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InventoryBalanceModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var inventoryBalance = await _serviceManager.InventoryBalanceService.GetById(id);
                if (inventoryBalance == null)
                    return NotFound();

                return Ok(inventoryBalance);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching InventoryBalance by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/InventoryBalance/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<InventoryBalanceModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var inventoryBalance = await _serviceManager.InventoryBalanceService.SearchInventoryBalancesByKeywordAsync(keyword);
                return Ok(inventoryBalance);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching InventoryBalances", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
