using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IMovingRequestRepository
    {

        Task<IEnumerable<MovingRequestDto>> GetAllAsync();
        Task<MovingRequestDto> GetByIdAsync(int id);
        Task AddAsync(MovingRequestDto movingRequesDto);
        Task UpdateAsync(MovingRequestDto movingRequesDto);
        Task DeleteAsync(int id);
    }
}
