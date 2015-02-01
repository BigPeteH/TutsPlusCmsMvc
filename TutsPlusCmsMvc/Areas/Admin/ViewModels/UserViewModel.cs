using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutsPlusCmsMvc.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [DisplayName("Display Name")]
        [Required]
        public string DisplayName { get; set; }

        [DisplayName("Current Password")]
        public string CurrentPassword { get; set; }

        [DisplayName("New Password")]
        [Compare("ConfirmPassword", ErrorMessage = "The new password and confirm password do not match.")]
        public string NewPassword { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        
    }
}