using System.ComponentModel.DataAnnotations;

namespace Endpoint_WebApp.Models.ViewModels.Authentication
{
    public class SignUpViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }
        public string NurseNumber { get; set; }
        public short NurseWorkYear { get; set; }
        [Required]
        public List<ulong> NurseDoService { get; set; }
        public string Shift { get; set; }
    }
}
