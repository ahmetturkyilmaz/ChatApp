using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Chat.API.Data;
using Chat.API.Exceptions;
using Chat.API.Helpers;
using Chat.API.Mapper;
using Chat.API.Models;
using Chat.API.Models.request;
using Chat.API.Repository.impl;
using Chat.API.Services.impl;
using MassTransit.Util.Scanning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Chat.API.Test
{
    class RoomServiceTest
    {
        private static DbContextOptions<DatabaseContext> dbContextOptions =
            new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "ChatAppDbRoomTest")
                .Options;

        private UserService _userService;
        private RoomService _roomService;
        private DatabaseContext _context;

        [OneTimeSetUp]
        public async Task Setup()
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
                FirstName = "Ahmet",
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
        public async Task TestUniqueRoomName()
        {
            var roomResponse = await _roomService.SaveRoom(1, new RoomDto()
            {
                Name = "Room 5"
            });

            Assert.ThrowsAsync<RoomNameAlreadyExistsException>(() => _roomService.SaveRoom(2, new RoomDto()
            {
                Name = "Room 5"
            }));
            Assert.IsTrue(roomResponse.Id == 1);
            Assert.AreEqual(roomResponse.Name, "Room 5");
        }

        [Test, Order(3)]
        public async Task TestGetById()
        {
            var roomResponse = await _roomService.GetById(1);
            Assert.IsTrue(roomResponse.Id == 1);
            Assert.AreEqual(roomResponse.Name, "Room 5");
        }

        [Test, Order(4)]
        public async Task TestDeleteById()
        {
            await _roomService.DeleteRoom(1);

            Assert.ThrowsAsync<RoomNotFoundException>(() => _roomService.GetById(1));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
    }
}