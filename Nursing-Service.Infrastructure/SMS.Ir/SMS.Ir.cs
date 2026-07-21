using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

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

        public async Task<SendSMSByUrlResponseModel> SendSmsAsync(string mobile)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("x-api-key", _config.Token);

            VerifySendModel model = new VerifySendModel()
            {
                Mobile = mobile,
                // TODO: Hard Code Template Id provided 
                TemplateId = 100000,
                Parameters = new VerifySendParameterModel[] {
                    new VerifySendParameterModel {
                        Name = "CODE", Value = "1234"
                    }
                }
            };

            string payload = JsonConvert.SerializeObject(model);
            StringContent stringContent = new(payload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(
                $"{BaseUrl}/send/verify", 
                stringContent
            );

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SendSMSByUrlResponseModel>(jsonResponse);

            return result!;
        }
    }
}
