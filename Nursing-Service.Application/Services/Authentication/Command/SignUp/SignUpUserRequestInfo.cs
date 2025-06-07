namespace Nursing_Service.Application.Services.Authentication.Command.SignUp
{
    public class SignUpUserRequestInfo
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string UserName { get; set; }
        public required string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
