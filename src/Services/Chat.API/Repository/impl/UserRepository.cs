using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chat.API.Data;
using Chat.API.Entities;
using Chat.API.Exceptions;
using Chat.API.Helpers;
using Chat.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Chat.API.Repository.impl
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<User> _db;
        private readonly IMapper _mapper;

        public UserRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _db = _context.Set<User>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            IQueryable<User> query = _db;
            query = query.Where(user => true);
            var result = await query.ToListAsync();
            return _mapper.Map<List<UserDto>>(result);
        }

        public async Task<UserDto> GetUserById(int id)
        {
            IQueryable<User> query = _db;
            query = query.Where(user => user.Id == id);
            var result = await query.FirstOrDefaultAsync();

            if (result == null)
            {
                throw new UserNotFoundException();
            }

            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            IQueryable<User> query = _db;

            query = query.Where(user => user.Email == email);
            var result = await query.FirstOrDefaultAsync();
            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> CreateUser(UserDto user)
        {
            var storedUsers = await GetUserByEmail(user.Email);
            if (storedUsers != null)
            {
                throw new UserAlreadyExistsException();
            }

            var entity = _mapper.Map<User>(user);
            await _db.AddAsync(entity);
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> UpdateUser(UserDto user)
        {
            var entity = _mapper.Map<User>(user);
            var updateResult = _db.Attach(entity);
            _context.Entry(user).State = EntityState.Modified;
            return _mapper.Map<UserDto>(updateResult.Entity);
        }

        public async Task DeleteUser(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }
    }
}