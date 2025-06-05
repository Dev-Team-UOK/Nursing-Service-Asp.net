namespace Nursing_Service.Application.Services.Users.Query
{
    public class GetUserResultDTO
    {
        public required string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
