using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chat.API.Data;
using Chat.API.Entities;
using Chat.API.Exceptions;
using Chat.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.API.Repository.impl
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<Room> _db;
        private readonly IMapper _mapper;

        public RoomRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _db = _context.Set<Room>();
            _mapper = mapper;
        }

        public async Task<RoomDto> GetByRoomName(int userId, string name)
        {
            IQueryable<Room> query = _db;
            var rooms = await query
                .Where(r => r.Name.Equals(name))
                .FirstOrDefaultAsync();
            if (rooms==null)
            {
                return null;
            }
            return _mapper.Map<RoomDto>(rooms);
        }

        public async Task<RoomDto> Get(int id)
        {
            IQueryable<Room> query = _db;
            var result = await query.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                throw new RoomNotFoundException();
            }

            return _mapper.Map<RoomDto>(result);
        }

        public async Task<Room> SaveRoom(RoomDto room)
        {
            room.CreatedAt = new DateTime();
            var roomEntity = new Room()
            {
                Name = room.Name,
                CreatedAt = new DateTime()
            };
            await _db.AddAsync(roomEntity);
            await _context.SaveChangesAsync();
            return roomEntity;
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            if (entity == null)
            {
                throw new RoomNotFoundException();
            }

            _db.Remove(entity);
            _context.SaveChanges();
        }

        public async Task Update(RoomDto roomDto)
        {
            var result = _mapper.Map<Room>(roomDto);
            _db.Attach(result);
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}