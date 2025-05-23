namespace Nursing_Service.Infrastructure.SMS.Ir
{
    public interface ISMSIr
    {
        Task<SendSMSByUrlResponseModel> SendSmsAsync(string mobile, string message);
    }
}
