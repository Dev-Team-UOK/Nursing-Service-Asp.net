namespace Nursing_Service.Domain.Entities.SuperVisor
{
    public class SuperVisor : User.User
    {
        public Shift Shift { get; set; }
        public virtual List<Nurse.Nurse> SupervisorNurses { get; set; }
    }
}
