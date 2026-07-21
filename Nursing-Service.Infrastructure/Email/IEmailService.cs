namespace Nursing_Service.Infrastructure.Email
{
    public interface IEmailService
    {
        Task<SendEmailResponseModel> SendEmailAsync(string to, string subject, string body);
    }
}
