﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chat.API.Models;

namespace Chat.API.Entities
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}