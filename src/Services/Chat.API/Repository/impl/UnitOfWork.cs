using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chat.API.Data;

namespace Chat.API.Repository.impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IMessageRepository _messageRepository;
        private IRoomRepository _roomRepository;
        private IUserRepository _userRepository;
        private IRoomUserRepository _roomUserRepository;
        private IMapper _mapper;

        public UnitOfWork(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IMessageRepository MessageRepository => _messageRepository ??= new MessageRepository(_context, _mapper);
        public IRoomRepository RoomRepository => _roomRepository ??= new RoomRepository(_context, _mapper);
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context, _mapper);
        public IRoomUserRepository RoomUserRepository => _roomUserRepository ??= new RoomUserRepository(_context,_mapper);



        /*
        public IMessageRepository MessageRepository
        {
            get { return new MessageRepository(_context, _mapper); }
        }

        public IRoomRepository RoomRepository
        {
            get { return new RoomRepository(_context, _mapper); }
        }

        public IUserRepository UserRepository
        {
            get { return new UserRepository(_context, _mapper); }
        }*/
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}