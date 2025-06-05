using Nursing_Service.Application.Services.PatinetNeedService.Query.GetPatientNeedServices;
using Nursing_Service.Domain.Entities.Patient;

namespace Nursing_Service.Application.Services.Patient.Query.GetPatients
{
    public class GetPatientsResultDTO
    {
        public ulong Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public uint Age { get; set; }
        public uint? Height { get; set; }
        public uint? Weight { get; set; }
        public string Address { get; set; }
        public GenderEnum Gender { get; set; }
        public string? IllnessHistory { get; set; }
        public List<GetPatientNeedServiceResultDTO>? Services { get; set; }
    }
}
