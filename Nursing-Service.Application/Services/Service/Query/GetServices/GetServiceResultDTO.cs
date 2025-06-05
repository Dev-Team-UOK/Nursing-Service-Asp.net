namespace Nursing_Service.Application.Services.Service.Query.GetServices
{
    public class GetServiceResultDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public short? MinDuration { get; set; }
    }
}
