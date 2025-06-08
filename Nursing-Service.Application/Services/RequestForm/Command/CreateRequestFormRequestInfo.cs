namespace Nursing_Service.Application.Services.RequestForm.Command
{
    public class CreateRequestFormRequestInfo
    {
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public string ServiceName { get; set; }
    }
}
