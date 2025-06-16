using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    public class MovingRequestRepository : IMovingRequestRepository
    {
        private readonly AppDbContext _context;
        public MovingRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovingRequestDto>> GetAllAsync()
        {

            var movingRequest = await _context.MovingRequest.ToListAsync();
            return movingRequest.Select(b => new MovingRequestDto
            {
                Id = b.Id,
                CustomerName = b.CustomerName,
                CustomerEmail = b.CustomerEmail,
                CustomerPhone = b.CustomerPhone,
                PickupAddress = b.PickupAddress,
                DeliveryAddress = b.DeliveryAddress,
                MovingDate = b.MovingDate,
                MovingType = b.MovingType,
                Status = b.Status

            });

        }

        public async Task<MovingRequestDto> GetByIdAsync(int id)
        {
            var movingRequest = await _context.MovingRequest.FindAsync(id);
            if (movingRequest == null) return null!;
            return new MovingRequestDto
            {
                Id = movingRequest.Id,
                CustomerName = movingRequest.CustomerName,
                CustomerEmail = movingRequest.CustomerEmail,
                CustomerPhone = movingRequest.CustomerPhone,
                PickupAddress = movingRequest.PickupAddress,
                DeliveryAddress = movingRequest.DeliveryAddress,
                MovingDate = movingRequest.MovingDate,
                MovingType = movingRequest.MovingType,
                Status = movingRequest.Status
            };
        }

        public async Task AddAsync(MovingRequestDto movingRequestDto)
        {
            try
            {
                var entity = new MovingRequest
                {
                    CustomerName = movingRequestDto.CustomerName,
                    CustomerEmail = movingRequestDto.CustomerEmail,
                    CustomerPhone = movingRequestDto.CustomerPhone,
                    PickupAddress = movingRequestDto.PickupAddress,
                    DeliveryAddress = movingRequestDto.DeliveryAddress,
                    MovingDate = movingRequestDto.MovingDate,
                    MovingType = movingRequestDto.MovingType,
                    Status = movingRequestDto.Status
                };

                _context.MovingRequest.Add(entity);
                await _context.SaveChangesAsync();

                // Set the generated ID back to the DTO
                movingRequestDto.Id = entity.Id;
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new Exception($"Error saving moving request: {innerMessage}", dbEx);
            }
        }



        public async Task UpdateAsync(MovingRequestDto movingRequesDto)
        {
            var entity = _context.MovingRequest.Find(movingRequesDto.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Moving request with ID {movingRequesDto.Id} not found.");
            }
            entity.CustomerName = movingRequesDto.CustomerName;
            entity.CustomerEmail = movingRequesDto.CustomerEmail;
            entity.CustomerPhone = movingRequesDto.CustomerPhone;
            entity.PickupAddress = movingRequesDto.PickupAddress;
            entity.DeliveryAddress = movingRequesDto.DeliveryAddress;
            entity.MovingDate = movingRequesDto.MovingDate;
            entity.MovingType = movingRequesDto.MovingType;
            entity.Status = movingRequesDto.Status;
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.MovingRequest.FindAsync(id);
            if (entity != null)
            {
                _context.MovingRequest.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }
}
