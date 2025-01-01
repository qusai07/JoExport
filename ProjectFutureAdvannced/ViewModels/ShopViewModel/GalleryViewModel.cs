using System.ComponentModel.DataAnnotations;

namespace ProjectFutureAdvannced.ViewModels.ShopViewModel
    {
    public class GalleryViewModel
        {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public FormFile? ImageFile { get; set; }
        }
    }
