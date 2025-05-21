using Nursing_Service.Domain.Entities.Patient;
using Nursing_Service.Domain.Entities.SuperVisor;

namespace Nursing_Service.Domain.Entities.Nurse
{
    public class Nurse : User.User
    {
        public virtual SuperVisor.SuperVisor SuperVisor { get; set; }
        public ulong SuperVisorId { get; set; }
        public List<PatientNeedService> ServicesForPatients { get; set; }
        public short? WorkHistoryInYear { get; set; }
        public DateTime StartWorkingInCompany { get; set; } = DateTime.Now;
        public virtual List<NurseCanDoService> DoService { get; set; }
    }
}
