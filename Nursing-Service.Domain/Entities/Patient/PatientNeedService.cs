using Nursing_Service.Domain.Entities.Base;

namespace Nursing_Service.Domain.Entities.Patient
{
    public class PatientNeedService : BaseEntity
    {
        public DateTime ServiceDateTime { get; set; }
        public bool IsDone { get; set; } = false;
        public bool IsPast { get; set; } = false;
        public virtual SuperVisor.SuperVisor AssignSuperVisor { get; set; }
        public ulong SuperVisorId { get; set; }
        public virtual Nurse.Nurse? AssignNurse{ get; set; }
        public ulong? NurseId { get; set; }
        public virtual Service.Service Service { get; set; }
        public ulong ServiceId { get; set; }
        public virtual Patient Patient { get; set; }
        public ulong PatientId { get; set; }    
    }
}
