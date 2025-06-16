using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;


namespace Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync()

        {
            var services = await _context.Services.ToListAsync();
            return services.Select(s => new ServiceDto
            {
                Id = s.Id,
                NameofService = s.Name,
                DescriptionofService = s.Description,
                PriceofService = s.Price
            });


        }

        public async Task<ServiceDto> GetByIdAsync(int id)
        {
            var services = await _context.Services.FindAsync(id);
            if (services == null) return null!;

            return new ServiceDto
            {
                Id = services.Id,
                NameofService = services.Name,
                DescriptionofService = services.Description,
                PriceofService = services.Price
            };
        }

        public async Task AddAsync(int userId, ServiceDto serviceDto)
        {
            try
            {
                var entity = new Service
                {
                    Name = serviceDto.NameofService,
                    Description = serviceDto.DescriptionofService,
                    Price = serviceDto.PriceofService,

                };

                _context.Services.Add(entity);
                await _context.SaveChangesAsync();

                serviceDto.Id = entity.Id;
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new Exception($"Error saving service: {innerMessage}", dbEx);
            }
        }

        public async Task UpdateAsync(ServiceDto serviceDto)
        {
            var entity = await _context.Services.FindAsync(serviceDto.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Service with ID {serviceDto.Id} not found.");
            }

            // Update the entity with new values
            entity.Name = serviceDto.NameofService;
            entity.Description = serviceDto.DescriptionofService;
            entity.Price = serviceDto.PriceofService;

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Services.FindAsync(id);
            if (entity != null)
            {
                _context.Services.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }








    }
}
