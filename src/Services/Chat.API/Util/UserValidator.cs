using System.Text.RegularExpressions;
using Chat.API.Entities;
using Chat.API.Exceptions;
using Chat.API.Models;


namespace Chat.API.Util
{
    public class UserValidator
    {
        public static void ValidateUser(UserDto user)
        {
            CheckIfPasswordProper(user.PasswordHash);
        }
        private static void CheckIfPasswordProper(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new RequestValidationException();
            }
            var regex  = new Regex( "(?=.*[0-9]{2})(?=.*[a-z]).{8,32}");
            if (!regex.IsMatch(password)){
                throw new RequestValidationException();
            }
        }
    }
}
