using System.ComponentModel.DataAnnotations;

namespace Nursing_Service.Application.Services.Users.Commands.Create
{
    public class CreateUserRequestInfo
    {
        [Required]
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}