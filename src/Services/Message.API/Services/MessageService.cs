using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Message.API.Data;
using Message.API.Repository;

namespace Message.API.Services
{
    public class MessageService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly GenericRepository<Message> _repository;
        
        public MessageService(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void GetAllMessages(int roomId)
        {
            
        }
    }
}