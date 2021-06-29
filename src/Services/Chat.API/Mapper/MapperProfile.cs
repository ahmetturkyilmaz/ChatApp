using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chat.API.Entities;
using Chat.API.Messaging;
using Chat.API.Models;
using EventBus.Messages.Events;

namespace Chat.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<MessageDto, MessageSendEvent>().ReverseMap();
        }
    }
}