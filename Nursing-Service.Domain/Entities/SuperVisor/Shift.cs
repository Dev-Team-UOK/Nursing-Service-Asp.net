using System.ComponentModel.DataAnnotations;

namespace Nursing_Service.Domain.Entities.SuperVisor
{
    public enum Shift
    {
        [Display(Name = "صبح")]
        Morning,
        [Display(Name = "عصر")]
        Evening,
        [Display(Name = "شب")]
        Night
    }
}
