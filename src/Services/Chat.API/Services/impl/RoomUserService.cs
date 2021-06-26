using System.Threading.Tasks;
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


        public async Task PostRoomUser( int userId, int roomId)
        {
            await _repository.PostRoomUser(userId, roomId);
            await _unitOfWork.Save();
        }
    }
}