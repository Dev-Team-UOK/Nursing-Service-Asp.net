namespace Nursing_Service.Infrastructure.SMS.Ir
{
    public class SendSMSByUrlResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public SendSMSByUrlDatResponseModel Data { get; set; }
    }

    public class SendSMSByUrlDatResponseModel
    {
        public long MessageId { get; set; }
        public decimal Cost { get; set; }
    }
}
