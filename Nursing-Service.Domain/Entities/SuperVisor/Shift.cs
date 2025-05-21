using System.ComponentModel.DataAnnotations;

namespace Nursing_Service.Domain.Entities.SuperVisor
{
    public enum Shift
    {
        [Display(Name = "صبح")]
        Morning,
        [Display(Name = "ظهر")]
        noon,
        [Display(Name = "شب")]
        Night
    }
}
