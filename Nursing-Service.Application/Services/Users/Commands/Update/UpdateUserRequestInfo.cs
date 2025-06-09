using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nursing_Service.Application.Services.Users.Commands.Update
{
    public class UpdateUserRequestInfo
    {
        public ulong Id { get; set; }
        [DisplayName("نام کاربری")]
        public string? UserName { get; set; }
        [DisplayName("نام")]
        public string? FirstName { get; set; }
        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }
        [DisplayName("شماره همراه")]
        public string? PhoneNumber { get; set; }
        [DisplayName("ایمیل")]
        public string? Email { get; set; }
        [DisplayName("رمز عبور")]
        public string? Password { get; set; }
    }
}
