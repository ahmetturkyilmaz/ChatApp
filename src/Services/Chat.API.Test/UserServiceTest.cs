using System.Threading.Tasks;
using AutoMapper;
using Chat.API.Data;
using Chat.API.Entities;
using Chat.API.Helpers;
using Chat.API.Mapper;
using Chat.API.Models;
using Chat.API.Models.request;
using Chat.API.Repository;
using Chat.API.Repository.impl;
using Chat.API.Services.impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Chat.API.Test
{
    public class Tests
    {
        private static DbContextOptions<DatabaseContext> dbContextOptions =
            new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "ChatAppDbUserTest")
                .Options;

        private UserService _userService;
        private RoomService _roomService;
        private DatabaseContext _context;

        [OneTimeSetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(opts => opts.AddProfile(new MapperProfile()));
            IOptions<AppSettings> someOptions =
                Options.Create<AppSettings>(new AppSettings() {Secret = "ASecretOfanExhoustedMan"});
            _context = new DatabaseContext(dbContextOptions);
            _context.Database.EnsureCreated();


            var mapper = config.CreateMapper();

            _roomService = new RoomService(new UnitOfWork(_context, new AutoMapper.Mapper(config)));
            _userService = new UserService(new UnitOfWork(_context, new AutoMapper.Mapper(config)), someOptions);
        }

        [Test, Order(1)]
        public async Task TestAuthenticate()
        {
            var user = await _userService.Create(new SignupRequest()
            {
                Email = "ahmet@gmail.com",
                Name = "Ahmet",
                LastName = "Turkyilmaz",
                Password = "ahmet123456"
            });
            var jwtResponse = await _userService.Authenticate(new LoginRequest()
            {
                Email = user.Email,
                Password = "ahmet123456"
            });

            Assert.IsTrue(jwtResponse.AccessToken != null);
        }

        [Test, Order(2)]
        public async Task GetUser()
        {
            var user = await _userService.GetById(1);

            Assert.AreEqual(user.Email, "ahmet@gmail.com");
            Assert.AreEqual(user.Name, "Ahmet");
            Assert.AreEqual(user.Surname, "Turkyilmaz");
        }

        [Test, Order(3)]
        public async Task TestGetUserWithRooms()
        {
            var roomRes = await _roomService.SaveRoom(1, new RoomDto()
            {
                Name = "Room 1"
            });
            var roomRes2 = await _roomService.SaveRoom(1, new RoomDto()
            {
                Name = "Room 2"
            });
            var responses = await _userService.GetUserRooms(1);
            Assert.IsTrue(responses.Count == 2);
            Assert.IsTrue(responses[0].Name.Equals("Room 1"));
            Assert.IsTrue(responses[1].Name.Equals("Room 2"));
        }
        [Test, Order(4)]
        public async Task CreateMultipleUsers()
        {
            var user = await _userService.Create(new SignupRequest()
            {
                Email = "turkyilmazah@gmail.com",
                Name = "Ahmet",
                LastName = "Turkyilmaz",
                Password = "ahmet12345678"
            });
            var jwtResponse = await _userService.Authenticate(new LoginRequest()
            {
                Email = user.Email,
                Password = "ahmet12345678"
            });

            Assert.IsTrue(jwtResponse.AccessToken != null);
            var roomRes = await _roomService.SaveRoom(2, new RoomDto()
            {
                Name = "Room 3"
            });
            var roomRes2 = await _roomService.SaveRoom(2, new RoomDto()
            {
                Name = "Room 4"
            });
            var responses = await _userService.GetUserRooms(2);
            Assert.IsTrue(responses.Count == 2);
            Assert.IsTrue(responses[0].Name.Equals("Room 3"));
            Assert.IsTrue(responses[1].Name.Equals("Room 4"));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
    }
}