using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _ICustomerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _ICustomerRepository = customerRepository;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _ICustomerRepository.GetAllAsync();
            return Ok(customers);


        }
        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _ICustomerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CustomerDto customer)
        {
            if (customer == null)
            {
                return BadRequest("Service data is null.");
            }
            try
            {
                // Replace 0 with actual userId if available
                await _ICustomerRepository.AddAsync(customer);
                return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                // Optional: Log the exception here
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, CustomerDto customer)
        {
            if (id != customer.Id) return BadRequest("Customer ID mismatch.");
            await _ICustomerRepository.UpdateAsync(customer);
            return NotFound();
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _ICustomerRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}

