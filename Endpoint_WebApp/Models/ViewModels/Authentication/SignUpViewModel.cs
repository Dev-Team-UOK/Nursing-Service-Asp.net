﻿using System.ComponentModel.DataAnnotations;

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
    }
}
