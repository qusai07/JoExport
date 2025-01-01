using ProjectFutureAdvannced.Models.Enums;
using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using ProjectFutureAdvannced.ViewModels.AdminViewModel;
using ProjectFutureAdvannced.ViewModels.ProductViewModel;
using ProjectFutureAdvannced.ViewModels.ShopViewModel;
using ProjectFutureAdvannced.ViewModels.UserViewModel;

namespace ProjectFutureAdvannced.ViewModels
{
    public class ListOfInfo
        {
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Gallery> galleries { get; set; }

        public IEnumerable<Card> cards { get; set; }
        public IEnumerable<Shop> shops { get; set; }
        public Account carrentUser { get; set; }
        public GeneralInfoUser generalInfoUser { get; set; }
        public string categorys { get; set; }
        public AppUser AppUser { get; set; }
        public PostViewModel PostViewModel { get; set; }
        public GalleryViewModel GalleryViewModel { get; set; }
        public SearchProduct SearchProduct { get; set; }
        public Shop Shop { get; set; } 
        public IEnumerable<AppUser> appUsers { get; set; }  


        }
    }
