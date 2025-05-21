using System.ComponentModel.DataAnnotations;

namespace Nursing_Service.Domain.Entities.Patient
{
    public enum GenderEnum
    {
        [Display(Name = "مرد")]
        Male,
        [Display(Name = "زن")]
        Female
    }
}
