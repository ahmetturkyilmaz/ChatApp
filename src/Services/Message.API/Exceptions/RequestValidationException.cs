using System;
using System.Runtime.Serialization;

namespace Message.API.Exceptions
{
    [Serializable]
    internal class RequestValidationException : System.Exception
    {
        public RequestValidationException()
        {
        }

        public RequestValidationException(string message) : base(message)
        {
        }

        public RequestValidationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected RequestValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}