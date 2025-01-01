using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectFutureAdvannced.Models.Model.AccountUser
    {
    public class Account
        {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string ?Major { get; set; }
        [RegularExpression(@"^(?i)(male|female)$")]
        public string? Gender { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm Password & Password is not Match!")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [RegularExpression(@"(77|79|78)\d{7}")]
        public string? PhoneNumber { get; set; }
        }
    }
