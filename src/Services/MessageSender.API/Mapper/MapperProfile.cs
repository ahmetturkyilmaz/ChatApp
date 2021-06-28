
using AutoMapper;
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