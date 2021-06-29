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

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _messageRepository = unitOfWork.MessageRepository;
        }

        public async Task<List<MessageDto>> GetAllMessagesByRoomId(int roomId)
        {
            var result = await _messageRepository.GetAllByRoomId(roomId);
            return result;
        }

        public async Task<List<MessageDto>> GetByPagination(int roomId, int now, int next)
        {
            var messages = await _messageRepository.GetByPagination(roomId, now, next);
            
            if (messages == null)
            {
                return null;
            }

            return messages;
        }

        public async Task<MessageDto> GetById(int messageId)
        {
            return await _messageRepository.Get(messageId);
        }

        public async Task<MessageDto> SaveMessage(MessageDto message)
        {
            message.Content = Regex.Replace(message.Content, @"(?i)<(?!img|a|/a|/img).*?>", string.Empty);

            var response = await _messageRepository.SaveMessage(message);

            var roomToBeUpdated = await _unitOfWork.RoomRepository.Get(response.ToRoomId);

            roomToBeUpdated.LastMessageAt = DateTime.Now;
            await _unitOfWork.RoomRepository.Update(roomToBeUpdated);
            await _unitOfWork.Save();
            return response;
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