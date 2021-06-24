using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Chat.API.Models;
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

        public async Task<MessageDto> GetById(int messageId)
        {
            return await _messageRepository.Get(messageId);
        }

        public async Task SaveMessage(MessageDto message)
        {
            RoomDto room = await _unitOfWork.RoomRepository.Get(message.ToRoomId);

            message.Content = Regex.Replace(message.Content, @"(?i)<(?!img|a|/a|/img).*?>", string.Empty);

            MessageDto storedMessage = await _messageRepository.SaveMessage(message);
            room.Messages.Add(storedMessage);

            await _unitOfWork.RoomRepository.SaveRoom(room);

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