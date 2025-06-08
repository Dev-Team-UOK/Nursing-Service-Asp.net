namespace Endpoint_WebApp.Models.ViewModels.Home
{
    public class CreateFormViewModel
    {
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public string ServiceName { get; set; }
    }
}
