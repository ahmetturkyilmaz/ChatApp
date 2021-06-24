using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Message.API.Exceptions;
using Message.API.Models;
using Message.API.Repository;

namespace Message.API.Services.impl
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomRepository _repository;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.roomRepository;
        }

        public async Task<IList<RoomDto>> GetAllByUserId(int userId)
        {
            return await _repository.GetAllByUserId(userId);
        }

        public async Task<RoomDto> GetById(int roomId)
        {
            var result = await _repository.Get(roomId);
            if (result == null)
            {
                throw new RoomNotFoundException();
            }

            return result;
        }

        public async Task SaveRoom(int userId, RoomDto room)
        {
            var storedRoom = _repository.GetByRoomName(userId, room.Name);
            if (storedRoom != null)
            {
                throw new RoomNameAlreadyExistsException();
            }

            await _repository.SaveRoom(room);
            await _unitOfWork.Save();

        }

        public async Task UpdateRoom(RoomDto room)
        {
            await _repository.Update(room);
            await _unitOfWork.Save();

        }

        public async Task DeleteRoom(int id)
        {
            await _repository.Delete(id);
        }
    }
}