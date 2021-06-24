using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepository MessageRepository { get; }

        IRoomRepository RoomRepository { get; }

        Task Save();
    }
}