using Microsoft.Build.Framework;

namespace ProjectFutureAdvannced.ViewModels.CategoryViewModel
    {
    public class CreateCategoryViewModel
        {
        [Required]
        public string Name { get; set; }
        public string? UrlImgString { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }
        }
    }
