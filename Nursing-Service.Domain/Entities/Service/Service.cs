using Nursing_Service.Domain.Entities.Base;
using Nursing_Service.Domain.Entities.Nurse;
using Nursing_Service.Domain.Entities.Patient;

namespace Nursing_Service.Domain.Entities.Service
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public short? MinDuration { get; set; }
        public virtual List<Patient.PatientNeedService> PatientNeedServices { get; set; }
        public virtual List<NurseCanDoService> NurseDoService { get; set; }
    }
}
