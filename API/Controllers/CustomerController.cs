using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;



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

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _ICustomerRepository.GetAllAsync();
            return Ok(customers);


        }
     
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

       
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CustomerDto customer)
        {
            if (customer == null)
            {
                return BadRequest("Service data is null.");
            }
            try
            {
               
                await _ICustomerRepository.AddAsync(customer);
                return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


       
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, CustomerDto customer)
        {
            if (id != customer.Id) return BadRequest("Customer ID mismatch.");
            await _ICustomerRepository.UpdateAsync(customer);
            return NotFound();
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _ICustomerRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}

