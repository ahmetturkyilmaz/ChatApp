﻿using System;
using System.Runtime.Serialization;

namespace Authentication.API.Exception
{
    [Serializable]
    internal class UserNotFoundException : System.Exception
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}