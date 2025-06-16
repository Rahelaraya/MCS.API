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

    public class CustomerReposiory : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerReposiory(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {

            var customers = await _context.Customer.ToListAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address

            });

        }


        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null) return null!;
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public async Task AddAsync(CustomerDto customerDto)
        {

            try
            {
                var entity = new Customer
                {
                    Name = customerDto.Name,
                    Email = customerDto.Email,
                    Phone = customerDto.Phone,
                    Address = customerDto.Address
                };
                _context.Customer.Add(entity);
                await _context.SaveChangesAsync();
                customerDto.Id = entity.Id;
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new Exception($"Error saving customer: {innerMessage}", dbEx);

            }

        }
        public async Task UpdateAsync(CustomerDto customerDto)
        {
            var entity = await _context.Customer.FindAsync(customerDto.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Customer with ID {customerDto.Id} not found.");
            }
            entity.Name = customerDto.Name;
            entity.Email = customerDto.Email;
            entity.Phone = customerDto.Phone;
            entity.Address = customerDto.Address;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Customer.FindAsync(id);
            if (entity != null)
            {
                _context.Customer.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }
}
