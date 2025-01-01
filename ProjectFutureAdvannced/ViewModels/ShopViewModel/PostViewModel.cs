using Microsoft.Build.Framework;

namespace ProjectFutureAdvannced.ViewModels.ShopViewModel
    {
    public class PostViewModel
        {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? DateTime { get; set; }
        public IFormFile? ImageFile { get; set; }
        }
    }
