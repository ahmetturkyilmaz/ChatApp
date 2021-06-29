using System.Threading.Tasks;
using Chat.API.Entities;
using Chat.API.Models;
using Chat.API.Repository;

namespace Chat.API.Services.impl
{
    public class RoomUserService : IRoomUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomUserRepository _repository;

        public RoomUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.RoomUserRepository;
        }


        public async Task PostRoomUser(InviteUserDto inviteUserDto,int roomId)
        {
            await _repository.PostRoomUser(inviteUserDto.UserId, roomId);
            await _unitOfWork.Save();
        }
    }
}