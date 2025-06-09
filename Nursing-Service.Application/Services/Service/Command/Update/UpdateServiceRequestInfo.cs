namespace Nursing_Service.Application.Services.Service.Command.Update
{
    public class UpdateServiceRequestInfo
    {
        public ulong? Id { get; set; }
        public string? Name { get; set; }
        public decimal? Cost { get; set; }
        public short? MinDuration { get; set; }
    }
}
