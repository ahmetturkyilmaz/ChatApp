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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Chat.API.Test
{
    class MessageServiceTest
    {
        private static DbContextOptions<DatabaseContext> dbContextOptions =
            new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "ChatAppDbMessageTest")
                .Options;

        private UserService _userService;
        private RoomService _roomService;
        private MessageService _messageService;
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
            _messageService = new MessageService(new UnitOfWork(_context, new AutoMapper.Mapper(config)));
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
            var jwtResponse1 = await _userService.Authenticate(new LoginRequest()
            {
                Email = user.Email,
                Password = "ahmet123456"
            });
            var user2 = await _userService.Create(new SignupRequest()
            {
                Email = "turkyilmazah@gmail.com",
                FirstName = "Ahmet",
                LastName = "Turkyilmaz",
                Password = "ahmet12345678"
            });
            var jwtResponse2 = await _userService.Authenticate(new LoginRequest()
            {
                Email = user2.Email,
                Password = "ahmet12345678"
            });

            Assert.IsTrue(jwtResponse1.AccessToken != null);
            Assert.IsTrue(jwtResponse2.AccessToken != null);
        }

        [Test, Order(2)]
        public async Task CreateRoomsAndInjectMessages()
        {
            var roomResponse1 = await _roomService.SaveRoom(1, new RoomDto() {Name = "Room 1"});
            var roomResponse2 = await _roomService.SaveRoom(1, new RoomDto() {Name = "Room 2"});
            var messageResponse = await _messageService.SaveMessage(new MessageDto()
                {Content = "hello1", FromUserId = 1, ToRoomId = roomResponse1.Id});
            var messageResponse2 = await _messageService.SaveMessage(new MessageDto()
                {Content = "hello2", FromUserId = 2, ToRoomId = roomResponse2.Id});
            
            Assert.AreEqual(messageResponse.Content, "hello1");
            Assert.AreEqual(messageResponse.ToRoomId, roomResponse1.Id);
            Assert.AreEqual(messageResponse.FromUserId, 1);
            Assert.AreEqual(messageResponse2.Content, "hello2");
            Assert.AreEqual(messageResponse2.ToRoomId, roomResponse2.Id);
            Assert.AreEqual(messageResponse2.FromUserId, 2);

        }

        [Test, Order(3)]
        public async Task TestGetByPagination()
        {
            var messageResponse3 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello3", FromUserId = 1, ToRoomId = 1 });
            var messageResponse4 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello4", FromUserId = 2, ToRoomId = 1 });
            var messageResponse5 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello5", FromUserId = 1, ToRoomId = 1 });
            var messageResponse6 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello6", FromUserId = 2, ToRoomId = 1 });
            var messageResponse7 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello7", FromUserId = 1, ToRoomId = 1 });
            var messageResponse8 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello8", FromUserId = 2, ToRoomId = 1});
            var messageResponse9 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello9", FromUserId = 1, ToRoomId = 1 });
            var messageResponse10 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello10", FromUserId = 2, ToRoomId = 1 });
            var messageResponse11 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello11", FromUserId = 1, ToRoomId =1 });
            var messageResponse12 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello12", FromUserId = 2, ToRoomId = 1});
            var messageResponse13 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello13", FromUserId = 1, ToRoomId = 1 });
            var messageResponse14 = await _messageService.SaveMessage(new MessageDto()
            { Content = "hello14", FromUserId = 2, ToRoomId = 1 });
            var response = await _messageService.GetByPagination(1, 3, 3);
            Assert.AreEqual(response.Count, 3);
            Assert.AreEqual(response[0].FromUserId, 1);
            Assert.AreEqual(response[0].Content,"hello5");
        }
    }
}