using System.Threading.Tasks;
using Chat.API.Entities;
using Chat.API.Exceptions;
using Chat.API.Models;
using Chat.API.Models.response;
using Chat.API.Repository;


namespace Chat.API.Services.impl
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomRepository _repository;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.RoomRepository;
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

        public async Task<RoomDto> SaveRoom(int userId, RoomDto room)
        {
            var storedRoom = await _repository.GetByRoomName(userId, room.Name);
            if (storedRoom != null)
            {
                throw new RoomNameAlreadyExistsException();
            }

            var recordedRoom = await _repository.SaveRoom(room);

            await _unitOfWork.RoomUserRepository.PostRoomUser(userId, recordedRoom.Id);
            await _unitOfWork.Save();

            return recordedRoom;
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