
using AutoMapper;
using EventBus.Messages.Events;
using MessageSender.API.Models;

namespace MessageSender.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
 
            CreateMap<MessageDto, MessageSendEvent>().ReverseMap();
        }
    }
}