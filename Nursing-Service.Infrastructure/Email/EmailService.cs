using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace Nursing_Service.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _config;

        public EmailService(IOptions<EmailConfig> config)
        {
            _config = config.Value;
        }

        public async Task<SendEmailResponseModel> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(_config.SenderEmail, _config.SenderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                message.To.Add(new MailAddress(to));

                using var client = new SmtpClient(_config.SmtpServer, _config.SmtpPort)
                {
                    Credentials = new NetworkCredential(_config.SenderEmail, _config.SenderPassword),
                    EnableSsl = true
                };

                await client.SendMailAsync(message);

                return new SendEmailResponseModel { IsSuccessful = true, Message = "ایمیل با موفقیت ارسال شد." };
            }
            catch (Exception ex)
            {
                return new SendEmailResponseModel { IsSuccessful = false, Message = ex.Message };
            }
        }
    }
}
