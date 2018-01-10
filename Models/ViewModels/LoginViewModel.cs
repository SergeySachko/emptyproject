using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
