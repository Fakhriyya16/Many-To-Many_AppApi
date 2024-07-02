using Service.DTOs.Admin.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IRoomService
    {
        Task CreateAsync(RoomCreateDto model);
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task DeleteAsync(int? id);
        Task EditAsync(int? id, RoomEditDto model);
        Task<RoomDto> GetByIdAsync(int id); 
    }
}
