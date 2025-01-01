using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Models.Enums;
using ProjectFutureAdvannced.Models.IRepository;
using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using ProjectFutureAdvannced.ViewModels;
using ProjectFutureAdvannced.ViewModels.ProductViewModel;

namespace ProjectFutureAdvannced.Controllers
    {
    [AllowAnonymous]

    public class HomeController : Controller
        {
        private readonly IShopRepository shopRepository;
        private readonly IProductRepository productRepository;
        private readonly UserManager<AppUser> _userManager;

        private readonly ICategoryRepository categoryRepository;
        public HomeController( UserManager<AppUser> _userManager, IProductRepository productRepository, ICategoryRepository categoryRepository, IShopRepository shopRepository )
        {
            this.categoryRepository = categoryRepository;
            this.shopRepository = shopRepository;
            this.productRepository = productRepository;
            this._userManager= _userManager;
        }
        public async Task<IActionResult> Index()
            {
            var appuser = await _userManager.Users.ToListAsync();
            ListOfInfo listOfInfo = new ListOfInfo()
                {
                appUsers = appuser,
                products=productRepository.GetAll(),
                categories = categoryRepository.GetAll()
                };
            return View( listOfInfo );
            }
        [HttpGet]
        public IActionResult Login()
            {
            return View();
            }
        [HttpGet]
        public IActionResult SignUp()
            {
            return View();
            }
        public IActionResult Category()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Store()
            {
            var product = productRepository.GetAll();
            ListOfInfo listOfInfo = new ListOfInfo()
                {
                products = product,
                };
            return View(listOfInfo);
            }
        [AllowAnonymous]

        public IActionResult StoreByCategory(string id )
            {
            var product = productRepository.GetAllByCategory(id);
            return View(product);
            }
        [AllowAnonymous]
        public IActionResult StoreBySearch( ListOfInfo model )
            {
            if (string.IsNullOrEmpty(model.SearchProduct.product) || model.SearchProduct.product.Length < 1)
                { 
                // Handle invalid input, e.g., return an empty list or an error message
                return View(productRepository.GetAll());
                }
            model.SearchProduct.product = model.SearchProduct.product.ToUpper(); // Convert to uppercase to ensure case-insensitive search
            var product = productRepository.StoreBySearch(model.SearchProduct.product[0]);
            return View(product);
            }
        //[AllowAnonymous]
        //[HttpGet]
        //public IActionResult StoreBySearch( List<Product> products)
        //    {
        //    return View(products);
        //    }
        public async Task<IActionResult> Details( int id )
            {
            var Product = productRepository.GetById(id);
            var Shop = shopRepository.Get(Product.ShopId);
            ProductViewModel productViewModel = new ProductViewModel()
                {
                Name = Product.Name,
                Description = Product.Description,
                CategoryName = Product.CategoryName,
                Price = Product.Price,
                Image = Product.Image,
                UserId=Shop.UserId,
                Id= id,
                };
            return View( productViewModel );
            }

        }
    }
