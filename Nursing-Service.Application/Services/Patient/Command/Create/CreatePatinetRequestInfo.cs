using Nursing_Service.Domain.Entities.Patient;

namespace Nursing_Service.Application.Services.Patient.Command.Create
{
    public class CreatePatinetRequestInfo
    {
        public required string NationalCode { get; set; }
        public required string FullName { get; set; }
        public required string Address { get; set; }
        public uint Age { get; set; } = 0;
        public GenderEnum Gender { get; set; }
        public uint? Height { get; set; }
        public uint? Weight { get; set; }
        public required string PhoneNumber { get; set; }
        public required string IllnessHistory { get; set; }
    }
}
