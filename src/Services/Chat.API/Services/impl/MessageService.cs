using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Chat.API.Models;
using Chat.API.Models.response;
using Chat.API.Repository;


namespace Chat.API.Services.impl
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IUnitOfWork unitOfWork, IRoomService roomService)
        {
            _messageRepository = unitOfWork.MessageRepository;
        }

        public async Task<IList<MessageDto>> GetAllMessagesByRoomId(int roomId)
        {
            await _unitOfWork.RoomRepository.Get(roomId);

            return await _messageRepository.GetAllByRoomId(roomId);
        }

        public async Task<List<MessageResponse>> GetByPagination(int roomId, int now, int next)
        {
            var messages = await _messageRepository.GetByPagination(roomId, now, next);
            if (messages == null)
            {
                return null;
            }

            return messages
                .Select(message =>
                    new MessageResponse(message.Id,
                        message.Content,
                        message.CreatedAt,
                        message.FromUserId,
                        message.FromUserId)).ToList();
        }

        public async Task<MessageDto> GetById(int messageId)
        {
            return await _messageRepository.Get(messageId);
        }

        public async Task SaveMessage(MessageDto message)
        {

            message.Content = Regex.Replace(message.Content, @"(?i)<(?!img|a|/a|/img).*?>", string.Empty);

            MessageResponse response = await _messageRepository.SaveMessage(message);

            await _unitOfWork.Save();
            //Broadcast the message back
        }

        public async Task UpdateMessage(MessageDto message)
        {
            await _messageRepository.Update(message);
            await _unitOfWork.Save();
        }

        public async Task DeleteMessage(int id)
        {
            await _messageRepository.Delete(id);
            await _unitOfWork.Save();
        }
    }
}