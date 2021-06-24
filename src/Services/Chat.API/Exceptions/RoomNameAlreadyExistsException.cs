using System;
using System.Runtime.Serialization;

namespace Chat.API.Exceptions
{
    [Serializable]
    internal class RoomNameAlreadyExistsException : System.Exception
    {
        public RoomNameAlreadyExistsException()
        {
        }

        public RoomNameAlreadyExistsException(string message) : base(message)
        {
        }

        public RoomNameAlreadyExistsException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected RoomNameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}