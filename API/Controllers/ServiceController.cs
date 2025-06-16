using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


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

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAll()
        {
            var Services = await _IServiceRepository.GetAllAsync();
            return Ok(Services);
        }


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

      

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ServiceDto service)
        {
            if (service == null)
            {
                return BadRequest("Service data is null.");
            }

            try
            {
                
                await _IServiceRepository.AddAsync(0, service);
                return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


       
        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateService(int id, ServiceDto service)
        {
            if (id != service.Id) return BadRequest("Service ID mismatch.");

            await _IServiceRepository.UpdateAsync(service);
            return NotFound();



        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _IServiceRepository.DeleteAsync(id);
            return NoContent();
        }


    }
}

