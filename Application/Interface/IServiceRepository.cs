using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IServiceRepository
    {
        Task<IEnumerable<ServiceDto>> GetAllAsync();
        Task<ServiceDto> GetByIdAsync(int id);
        Task AddAsync(int userId, ServiceDto serviceDto);
        Task UpdateAsync(ServiceDto serviceDto);
        Task DeleteAsync(int id);


    }
}
