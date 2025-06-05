using System.ComponentModel.DataAnnotations;

namespace Nursing_Service.Application.Services.Users.Commands.Update
{
    public class UpdateUserRequestInfo
    {
        public ulong Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
