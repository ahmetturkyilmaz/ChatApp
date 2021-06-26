using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chat.API.Data;
using Chat.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.API.Repository.impl
{
    public class RoomUserRepository : IRoomUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<RoomUser> _db;
        private readonly IMapper _mapper;

        public RoomUserRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _db = _context.Set<RoomUser>();
            _mapper = mapper;
        }

        public async Task PostRoomUser(int userId, int roomId)
        {
            await _db.AddAsync(new RoomUser()
            {
                UserId = userId,
                RoomId = roomId
            });
        }
    }
}