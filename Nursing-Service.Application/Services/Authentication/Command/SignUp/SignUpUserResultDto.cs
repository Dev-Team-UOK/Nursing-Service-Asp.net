using Nursing_Service.Domain.Entities.User;

namespace Nursing_Service.Application.Services.Authentication.Command.SignUp
{
    public class SignUpUserResultDto
    {
        public ulong Id { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public RoleEnum Role { get; set; }
    }
}
