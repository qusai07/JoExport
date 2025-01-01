using System.ComponentModel.DataAnnotations;

namespace ProjectFutureAdvannced.ViewModels.AdminViewModel
    {
    public class SecurityAdmin
        {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Confirm New Password & New Password is not Match!")]
        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
        }
    }
