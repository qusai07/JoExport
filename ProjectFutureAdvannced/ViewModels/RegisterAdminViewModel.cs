using System.ComponentModel.DataAnnotations;

namespace ProjectFutureAdvannced.ViewModels
    {
    public class RegisterAdminViewModel
        {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword in not Match")]
        public string ConfirmPassword { get; set; }
        }
    }
