using System;
using System.Runtime.Serialization;

namespace Authentication.API.Exception
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