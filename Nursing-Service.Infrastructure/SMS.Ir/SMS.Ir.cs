using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Nursing_Service.Infrastructure.SMS.Ir
{
    public class SMSIr : ISMSIr
    {
        private readonly SmsIrConfig _config;
        private string BaseUrl = "https://api.sms.ir/v1";

        public SMSIr(IOptions<SmsIrConfig> config)
        {
            _config = config.Value; 
        }

        public async Task<SendSMSByUrlResponseModel> SendSmsAsync(string mobile, string message)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync(
                $"{BaseUrl}send?username={_config.UserName}&password={_config.Token}&mobile={mobile}&line={_config.Line}&text={message}"
            );

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SendSMSByUrlResponseModel>(jsonResponse);

            return result!;
        }
    }
}
