using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Authentication.API.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PasswordHash { get; set; }
        
        public User()
        {
        }
        public User(string email, string name, string surname, string passwordHash)
        {
            Email = email;
            Name = name;
            Surname = surname;
            PasswordHash = passwordHash;
        }

        
    }
}
