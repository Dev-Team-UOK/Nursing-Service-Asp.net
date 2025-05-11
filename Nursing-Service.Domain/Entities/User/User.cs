using System.Diagnostics.CodeAnalysis;
using Nursing_Service.Domain.Entities.Base;

namespace Nursing_Service.Domain.Entities.User
{
    public class User : BaseEntity
    {
        [SetsRequiredMembers]
        public User(string userName, string? firstName, string? lastName, string phoneNumber, string email, string password)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
        }

        public User() { }

        public required string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
