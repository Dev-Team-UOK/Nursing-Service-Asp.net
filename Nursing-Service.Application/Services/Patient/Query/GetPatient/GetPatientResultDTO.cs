using Nursing_Service.Application.Services.Patient.Query.GetPatientNeedServices;
using Nursing_Service.Domain.Entities.Patient;

namespace Nursing_Service.Application.Services.Patient.Query.GetPatient
{
    public class GetPatientResultDTO
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public uint Age { get; set; }
        public uint? Height { get; set; }
        public uint? Weight { get; set; }
        public string Address { get; set; }
        public GenderEnum Gender { get; set; }
        public string? IllnessHistory { get; set; }
        public List<GetPatientNeedServicesResultDTO>? NeedServices { get; set; }
    }
}
