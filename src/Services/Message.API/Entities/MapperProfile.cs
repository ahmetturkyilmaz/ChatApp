using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Message.API.Models;

namespace Message.API.Entities
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<RoomUserDto, RoomUserDto>().ReverseMap();
        }
    }
}