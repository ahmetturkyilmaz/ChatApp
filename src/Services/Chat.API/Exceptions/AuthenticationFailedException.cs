using System;
namespace Chat.API.Exceptions
{
    [Serializable]
    internal class AuthenticationFailedException : System.Exception
    {
        public AuthenticationFailedException() : base()
        {
        }

        public AuthenticationFailedException(string message) : base(message)
        {
        }

    }
}