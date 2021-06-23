using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Message.API.Data;
using Message.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Message.API.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<Message> _db;
        private readonly IMapper _mapper;

        public RoomRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _db = _context.Set<Message>();
            _mapper = mapper;
        }
        public Task<IList<RoomDto>> GetAllByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<RoomDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RoomDto> SaveRoom(RoomDto room)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(RoomDto roomDto)
        {
            throw new NotImplementedException();
        }
    }
}