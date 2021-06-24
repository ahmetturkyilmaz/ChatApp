using System;
using System.Runtime.Serialization;

namespace Message.API.Exceptions
{
    [Serializable]
    internal class RoomNotFoundException : System.Exception
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