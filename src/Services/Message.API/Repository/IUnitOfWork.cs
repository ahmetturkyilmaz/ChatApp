using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.API.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepository messageRepository { get; }

        IRoomRepository roomRepository { get; }

        Task Save();
    }
}