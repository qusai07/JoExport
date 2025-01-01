using ProjectFutureAdvannced.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectFutureAdvannced.ViewModels.ProductViewModel
{
    public class CreateProduct
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Price { get; set; }
        public string? Image { get; set; }
        [Required]
        public IFormFile formFile { get; set; }
    }
}
