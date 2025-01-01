using System.ComponentModel.DataAnnotations;

namespace ProjectFutureAdvannced.ViewModels.UserViewModel
    {
    public class GeneralInfoUser
        {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        //[Required]
        //public TypeOfUser TypeOfRoles { get; set; }
        public IFormFile? ImgUser { get; set; }
        public string? UrlImgString { get; set; }
        [RegularExpression(@"(77|79|78)\d{7}")]
        public string? PhoneNumber { get; set; }
        [RegularExpression(@"^(?i)(male|female)$")]
        public string? Gender { get; set; }
        public string? Major { get; set; }
        }
    }
