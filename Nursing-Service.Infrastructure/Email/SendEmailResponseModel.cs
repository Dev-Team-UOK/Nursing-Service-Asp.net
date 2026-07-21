namespace Nursing_Service.Infrastructure.Email
{
    public class SendEmailResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
