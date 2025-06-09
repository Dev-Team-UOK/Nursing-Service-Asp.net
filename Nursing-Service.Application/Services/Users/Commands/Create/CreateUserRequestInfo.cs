using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nursing_Service.Application.Services.Users.Commands.Create
{
    public class CreateUserRequestInfo
    {
        [DisplayName("نام کاربری")]
        public required string UserName { get; set; }
        [DisplayName("نام")]
        public string? FirstName { get; set; }
        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }
        [DisplayName("شماره همراه")]
        public required string PhoneNumber { get; set; }
        [DisplayName("ایمیل")]
        public required string Email { get; set; }
        [DisplayName("رمز عبور")]
        public required string Password { get; set; }
    }
}