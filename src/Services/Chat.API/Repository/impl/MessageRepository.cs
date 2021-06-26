using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chat.API.Data;
using Chat.API.Entities;
using Chat.API.Models;
using Chat.API.Models.response;

namespace Chat.API.Repository.impl
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<Message> _db;
        private readonly IMapper _mapper;

        public MessageRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _db = _context.Set<Message>();
            _mapper = mapper;
        }

        public async Task<IList<MessageDto>> GetAllByRoomId(int roomId)
        {
            IQueryable<Message> query = _db;

            query = query.Where(m => m.ToRoomId == roomId)
                .Include(m => m.FromUserId)
                .Include(m => m.ToRoom)
                .OrderByDescending(m => m.CreatedAt)
                .Take(20)
                .Reverse();
            var messages = await query.AsNoTracking().ToListAsync();

            return _mapper.Map<IList<MessageDto>>(messages);
        }

        public async Task<MessageDto> Get(int id)
        {
            IQueryable<Message> query = _db;
            var result = await query.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<MessageDto>(result);
        }

        public async Task<List<MessageDto>> GetByPagination(int roomId, int now, int next)
        {
            IQueryable<Message> query = _db;
            var result = await query.Where(m => m.ToRoomId == roomId)
                .OrderByDescending(m => m.CreatedAt)
                .Skip(now)
                .Take(next)
                .ToListAsync();
            return _mapper.Map<List<MessageDto>>(result);
        }

        public async Task<MessageResponse> SaveMessage(MessageDto message)
        {
            message.CreatedAt = new DateTime();
            var entity = new Message()
            {
                Content = message.Content,
                CreatedAt = new DateTime(),
                ToRoomId = message.ToRoomId,
                FromUserId = message.FromUserId
            };
            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new MessageResponse(entity.Id, entity.Content, entity.CreatedAt, entity.FromUserId, entity.ToRoomId);
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public async Task Update(MessageDto messageDto)
        {
            var result = _mapper.Map<Message>(messageDto);
            _db.Attach(result);
            _context.Entry(result).State = EntityState.Modified;
        }
    }
}