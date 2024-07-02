using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Rooms;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepo;
        private readonly IMapper _mapper;
        public RoomService(IRoomRepository roomRepo,
                          IMapper mapper)
        {
            _roomRepo = roomRepo;
            _mapper = mapper;
        }
        public async Task CreateAsync(RoomCreateDto model)
        {
            await _roomRepo.CreateAsync(_mapper.Map<Room>(model));
        }

        public async Task DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var room = await _roomRepo.GetById((int)id);

            if (room is null) throw new NotFoundException("Room was not found");

            await _roomRepo.DeleteAsync(room);
        }

        public async Task EditAsync(int? id, RoomEditDto model)
        {
            if (id is null) throw new ArgumentNullException();

            var room = await _roomRepo.GetById((int)id);

            if (room is null) throw new NotFoundException("Room was not found");

            _mapper.Map(model, room);
            await _roomRepo.EditAsync(room);
        }

        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<RoomDto>>(await _roomRepo.GetAllAsync());
        }

        public async Task<RoomDto> GetByIdAsync(int id)
        {
            var room = await _roomRepo.GetById((int)id);

            if (room is null) throw new NotFoundException("Room was not found");

            return _mapper.Map<RoomDto>(room);
        }
    }
}
