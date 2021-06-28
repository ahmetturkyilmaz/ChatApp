using System;
using System.Runtime.Serialization;

namespace Chat.API.Exceptions
{
    [Serializable]
    public class RoomNotFoundException : System.Exception
    {
        public RoomNotFoundException()
        {
        }

        public RoomNotFoundException(string message) : base(message)
        {
        }

        public RoomNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected RoomNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}