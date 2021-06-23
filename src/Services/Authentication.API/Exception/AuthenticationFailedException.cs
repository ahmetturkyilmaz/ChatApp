using System;
namespace Authentication.API.Exception
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