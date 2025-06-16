using Application.DTO;
using Application.Interface;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovingRequestController : ControllerBase
    {
        private readonly IMovingRequestRepository _movingRequestRepository;
        public MovingRequestController(IMovingRequestRepository MovingRequestRepository)
        {
            _movingRequestRepository = MovingRequestRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var movingRequest = await _movingRequestRepository.GetAllAsync();
            return Ok(movingRequest);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var movingRequest = await _movingRequestRepository.GetByIdAsync(id);
            if (movingRequest == null)
            {
                return NotFound();
            }
            return Ok(movingRequest);
        }

      
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MovingRequestDto movingRequestDto)
        {
            if (movingRequestDto == null)
            {
                return BadRequest("Moving request data is null.");
            }
            try
            {
                await _movingRequestRepository.AddAsync(movingRequestDto);
                return CreatedAtAction(nameof(GetById), new { id = movingRequestDto.Id }, movingRequestDto);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatemovingRequest(int id, MovingRequestDto movingRequest)
        {
            if (id != movingRequest.Id) return BadRequest("Moving request ID mismatch.");

            await _movingRequestRepository.UpdateAsync(movingRequest);
            return NotFound();
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {


            await _movingRequestRepository.DeleteAsync(id);
            return NoContent();
        }


    }
}
