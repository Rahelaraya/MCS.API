using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase

    {

        private readonly IServiceRepository _IServiceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {

            _IServiceRepository = serviceRepository;
        }

        // GET: api/<ServiceController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAll()
        {
            var Services = await _IServiceRepository.GetAllAsync();
            return Ok(Services);
        }


        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetById(int id)
        {
            var service = await _IServiceRepository.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        // POST api/<ServiceController>

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ServiceDto service)
        {
            if (service == null)
            {
                return BadRequest("Service data is null.");
            }

            try
            {
                // Replace 0 with actual userId if available
                await _IServiceRepository.AddAsync(0, service);
                return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
            }
            catch (Exception ex)
            {
                // Optional: Log the exception here
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateService(int id, ServiceDto service)
        {
            if (id != service.Id) return BadRequest("Service ID mismatch.");

            await _IServiceRepository.UpdateAsync(service);
            return NotFound();



        }

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _IServiceRepository.DeleteAsync(id);
            return NoContent();
        }


    }
}

