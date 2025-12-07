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
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(InventoryTransactionController));

        public InventoryTransactionController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/InventoryTransaction
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InventoryTransactionModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var inventoryTransaction = await _serviceManager.InventoryTransactionService.GetAll();
                return Ok(inventoryTransaction);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching InventoryTransactions", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<InventoryTransactionModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var inventoryTransaction = await _serviceManager.InventoryTransactionService.GetAllPaged(pageNumber, pageSize);
                return Ok(inventoryTransaction);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/InventoryTransaction/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InventoryTransactionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var inventoryTransaction = await _serviceManager.InventoryTransactionService.GetById(id);
                if (inventoryTransaction == null)
                    return NotFound();

                return Ok(inventoryTransaction);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching InventoryTransaction by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/InventoryTransaction/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<InventoryTransactionModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var inventoryTransaction = await _serviceManager.InventoryTransactionService.SearchInventoryTransactionsByKeywordAsync(keyword);
                return Ok(inventoryTransaction);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching InventoryTransactions", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
