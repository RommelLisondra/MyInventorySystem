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
    public class CustomerController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(CustomerController));

        public CustomerController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        // GET api/customer
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customers = await _serviceManager.CustomerService.GetAll();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching customers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<CustomerModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllPaged(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var pagedcustomers = await _serviceManager.CustomerService.GetAllPaged(pageNumber, pageSize);
                return Ok(pagedcustomers);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error fetching paged warehouses", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/customer/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var customer = await _serviceManager.CustomerService.GetById(id);
                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error fetching customer by ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/customer
        [HttpPost]
        [ProducesResponseType(typeof(CustomerModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CustomerModel customerModel)
        {
            if (customerModel == null)
                return BadRequest("Customer data cannot be null.");

            try
            {
                var customerDto = _mapper.Map<CustomerDto>(customerModel);

                var createdBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.CustomerService.CreateCustomer(customerDto, createdBy);

                var returnedId = customerDto?.id ?? 0;

                var createdModel = _mapper.Map<CustomerModel>(customerDto);

                return CreatedAtAction(nameof(GetById), new { id = returnedId }, createdModel);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error creating customer", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/customer/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerModel customerModel)
        {
            if (customerModel == null || id != customerModel.id)
                return BadRequest("Invalid customer data.");

            try
            {
                var customerDto = _mapper.Map<CustomerDto>(customerModel);

                var editedBy = User?.Identity?.Name ?? "SystemUser";

                await _serviceManager.CustomerService.UpdateCustomer(customerDto, editedBy);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error updating customer with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/customer/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var customer = await _serviceManager.CustomerService.GetById(id);
                if (customer == null)
                    return NotFound();

                // Kung meron kang Delete method sa ICustomerService, call it here.
                // await _customerService.DeleteCustomer(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logCentral.Error($"Error deleting customer with ID: {id}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/customer/search?name=Juan
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<CustomerModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            try
            {
                var customers = await _serviceManager.CustomerService.SearchCustomersByKeywordAsync(keyword);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                logCentral.Error("Error searching customers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
