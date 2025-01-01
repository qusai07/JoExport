using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Models.Enums;
using ProjectFutureAdvannced.Models.IRepository;
using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using ProjectFutureAdvannced.Models.SqlRepository;
using ProjectFutureAdvannced.ViewModels;
using ProjectFutureAdvannced.ViewModels.AdminViewModel;
using ProjectFutureAdvannced.ViewModels.ProductViewModel;
using ProjectFutureAdvannced.ViewModels.ShopViewModel;
using ProjectFutureAdvannced.ViewModels.UserViewModel;

namespace ProjectFutureAdvannced.Controllers
    {
    public class ShopController : Controller
        {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IShopRepository _shopRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICartRepository cartRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IPostRepository postRepository;
        private readonly IGalleryRepository galleryRepository;
        private readonly IWishlistRRepository wishlistRepository;


        public ShopController( IWishlistRRepository wishlistRepository, IGalleryRepository galleryRepository, IPostRepository postRepository, IProductRepository productRepository, ICategoryRepository categoryRepository, ICartRepository cartRepository, IWebHostEnvironment webHostEnvironment, IShopRepository _shopRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> _roleManager )
            {
            _userManager = userManager;
            _signInManager = signInManager;
            this._roleManager = _roleManager;
            this._shopRepository = _shopRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.cartRepository = cartRepository;
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.postRepository = postRepository;
            this.galleryRepository= galleryRepository;
            this.wishlistRepository= wishlistRepository;
            }
        public IActionResult Index()
            {
            return View();
            }
        [HttpGet]
        public async Task<IActionResult> GeneralShopProfile()
            {
            GeneralInfoUser model;
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
                {
                var Shop = _shopRepository.GetByFk(user.Id);

                model = new GeneralInfoUser()
                    {
                    Name = Shop.Name,
                    Email = Shop.Email,
                    UrlImgString = user.ImgUrl,
                    PhoneNumber = Shop.PhoneNumber,
                    Gender = Shop.Gender,
                    Major = Shop.Major,
                    };
                return View(model);
                }
            return View();
            }
        /*************************************/
        [HttpPost]

        public async Task<IActionResult> GeneralShopProfile( GeneralInfoUser Shop )
            {
            if (ModelState.IsValid)
                {
                string uniqueFileName = null;
                var user = await _userManager.GetUserAsync(User);
                var shop = _shopRepository.GetByFk(user.Id);
                if (Shop.ImgUser != null)
                    {
                    string uniqueUpload = Path.Combine(webHostEnvironment.WebRootPath, "AccountImg");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Shop.ImgUser.FileName;
                    string filePath = Path.Combine(uniqueUpload, uniqueFileName);
                    await Shop.ImgUser.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    Shop.UrlImgString = uniqueFileName;
                    }
                else
                    {
                    if ((user.ImgUrl != null && Shop.Gender != null) || (user.ImgUrl == null && Shop.Gender != null))
                        {
                        if (Shop.Gender == "Male")
                            {
                            uniqueFileName = "img_avatar.png";
                            }
                        else
                          if (Shop.Gender == "Female")
                            {
                            uniqueFileName = "img_avatar2.png";
                            }
                        else
                            {
                            uniqueFileName = user.ImgUrl;
                            }
                        }
                    else
                    if ((user.ImgUrl != null && Shop.Gender == null))
                        {
                        uniqueFileName = user.ImgUrl;
                        }
                    Shop.UrlImgString = uniqueFileName;
                    }
                /*********************/
                int indexOfAt = Shop.Email.IndexOf("@");
                shop.Name = Shop.Name;
                user.ImgUrl = Shop.UrlImgString;
                shop.UserName = Shop.Email.Substring(0, indexOfAt);
                shop.Email = Shop.Email;
                /*************************/
                shop.Gender = Shop.Gender;
                shop.PhoneNumber = Shop.PhoneNumber;
                shop.Major = Shop.Major;
                _shopRepository.Update(shop);
                await _userManager.SetEmailAsync(user, user.Email);
                await _userManager.SetUserNameAsync(user, shop.Email.Substring(0, indexOfAt));
                await _userManager.UpdateAsync(user);
                return RedirectToAction("GeneralShopProfile", "Shop");
                }
            return View();
            }
        /***********************************/

        [HttpGet]
        public async Task<IActionResult> SecurityUser()
            {
            SecurityUser security = new SecurityUser();
            return View(security);
            }
        [HttpPost]
        public async Task<IActionResult> SecurityUser( SecurityUser security )
            {
            if (ModelState.IsValid)
                {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                    {
                    var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, security.CurrentPassword);

                    if (isCurrentPasswordValid)
                        {
                        if (security.NewPassword == security.ConfirmNewPassword)
                            {
                            var changePasswordResult = await _userManager.ChangePasswordAsync(user, security.CurrentPassword, security.NewPassword);

                            if (changePasswordResult.Succeeded)
                                {
                                await _signInManager.RefreshSignInAsync(user);
                                return RedirectToAction("SecurityUser", "Shop");
                                }
                            else
                                {
                                foreach (var error in changePasswordResult.Errors)
                                    {
                                    ModelState.AddModelError("", error.Description);
                                    }
                                return View(security);
                                }
                            }
                        }
                    else
                        {
                        ModelState.AddModelError("", "Current password is incorrect.");
                        }
                    }
                }

            return View();

            }
        [HttpGet]
        public IActionResult AddProduct()
            {
            return View();
            }

        public IActionResult DeleteProduct( int id )
            {
            //cartRepository.DeleteAllCardByProductId(id);
            //wishlistRepository.DeleteAllWishListByProductId(id);
            productRepository.Delete(id);
            return RedirectToAction("MyProduct", "Shop");
            }
        [HttpPost]
        public async Task<IActionResult> AddProduct( CreateProduct model )
            {
            if (ModelState.IsValid)
                {

                string uniqueFileName = null;
                string uniqueUpload = Path.Combine(webHostEnvironment.WebRootPath, "ProductImg");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.formFile.FileName;
                string filePath = Path.Combine(uniqueUpload, uniqueFileName);
                await model.formFile.CopyToAsync(new FileStream(filePath, FileMode.Create));
                model.Image = uniqueFileName;
                //var user = await _userManager.GetUserAsync(User);
                //  var category = categoryRepository.GetByCategoryName(model.CategoryName);
                // var shop = _shopRepository.Get(user.Id);
                Product product = new Product();
                product.Name = model.Name;
                product.Description = model.Description;
                product.CategoryName = model.CategoryName;
                product.Price = Convert.ToDouble(model.Price);
                product.Image = model.Image;
                var user = await _userManager.GetUserAsync(User);
                var shop = _shopRepository.GetByFk(user.Id);
                product.ShopId = shop.Id;
                productRepository.Add(product);
                return RedirectToAction("Index", "Home");
                }
            return View();
            }
        public async Task<IActionResult> MyProduct()
            {
            var user = await _userManager.GetUserAsync(User);
            var shop = _shopRepository.GetByFk(user.Id);
            var product = productRepository.Find(x => x.ShopId == shop.Id);
            return View(product);
            }
        [HttpGet]
        public IActionResult EditProduct( int id )
            {

            var product = productRepository.GetById(id);
            if (product == null)
                {
                return NotFound();
                }

            EditProductViewModel editProduct = new EditProductViewModel
                {
                Name = product.Name,
                Description = product.Description,
                CategoryName = product.CategoryName,
                Price = product.Price.ToString(),
                Image = product.Image,
                };

            return View(editProduct);
            }
        [HttpPost]
        public async Task<IActionResult> EditProduct( int id, EditProductViewModel model )
            {
            if (ModelState.IsValid)
                {
                var productToUpdate = productRepository.GetById(id);

                if (productToUpdate == null)
                    {
                    return NotFound();
                    }

                if (model.formFile != null)
                    {
                    if (!string.IsNullOrEmpty(productToUpdate.Image))
                        {
                        var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, "ProductImg", productToUpdate.Image);
                        if (System.IO.File.Exists(oldImagePath))
                            {
                            System.IO.File.Delete(oldImagePath);
                            }
                        }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.formFile.FileName;
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "ProductImg", uniqueFileName);
                    await model.formFile.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    productToUpdate.Image = uniqueFileName;
                    }
                productToUpdate.Name = model.Name;
                productToUpdate.Description = model.Description;
                productToUpdate.CategoryName = model.CategoryName;
                productToUpdate.Price = Convert.ToDouble(model.Price);
                productRepository.Update(productToUpdate);
                return RedirectToAction("MyProduct", "Shop");
                }
            return View();
            }


        [Authorize]
        public async Task<IActionResult> UserProfile( string UserName )
            {
            var user = await _userManager.FindByNameAsync(UserName);
            var shop = _shopRepository.GetByFk(user.Id);
            var posts =  postRepository.GetPostByShopId(shop.Id);
            var gallery = galleryRepository.GetPostByShopId(shop.Id);

            ListOfInfo listOfInfo = new ListOfInfo()
                {
                AppUser = user,
                Posts = posts,
                galleries = gallery,
                };
            return View(listOfInfo);
            }
        [HttpGet]
        public IActionResult EditPost( int id )
            {

            var post = postRepository.Get(id);
            if (post == null)
                {
                return NotFound();
                }

            PostViewModel editProduct = new PostViewModel
                {
                Name = post.Name,
                Description = post.Description,
                ImageUrl= post.ImageUrl,
                };

            return View(editProduct);
            }
        [HttpPost]
        public async Task<IActionResult> EditPost( int id, PostViewModel model )
            {
            if (ModelState.IsValid)
                {
                var PostToUpdate = postRepository.Get(id);

                if (PostToUpdate == null)
                    {
                    return NotFound();
                    }

                if (model.ImageFile != null)
                    {
                    if (!string.IsNullOrEmpty(PostToUpdate.ImageUrl))
                        {
                        var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, "ProductImg", PostToUpdate.ImageUrl);
                        if (System.IO.File.Exists(oldImagePath))
                            {
                            System.IO.File.Delete(oldImagePath);
                            }
                        }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "ProductImg", uniqueFileName);
                    await model.ImageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    PostToUpdate.ImageUrl = uniqueFileName;
                    }
                PostToUpdate.Name = model.Name;
                PostToUpdate.Description = model.Description;
                var user = await _userManager.GetUserAsync(User);
                return RedirectToAction("UserProfile", "Shop", new {UserName=user.UserName});
                }
            return View();
            }
        public async Task<IActionResult> DeletePost( int id )
            {
            postRepository.Delete(id);
            var user = await _userManager.GetUserAsync(User);
            return RedirectToAction("UserProfile", "Shop", new {UserName= user.UserName});
            }
        public IActionResult Create()
            {
            PostViewModel model = new PostViewModel();
            return PartialView("_PostPartialView", model);
            }
        [HttpPost]
        public async Task<IActionResult> Create( PostViewModel model )
            {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
                {
                string uniqueFileName = null;
                if (model.ImageFile != null)
                    {
                    string uniqueUpload = Path.Combine(webHostEnvironment.WebRootPath, "PostImage");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filePath = Path.Combine(uniqueUpload, uniqueFileName);
                    await model.ImageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    model.ImageUrl = uniqueFileName;
                    }

                //var user = await _userManager.GetUserAsync(User);
                //  var category = categoryRepository.GetByCategoryName(model.CategoryName);
                // var shop = _shopRepository.Get(user.Id);
                Post post = new Post();
                post.Name = model.Name;
                post.Description = model.Description;
                post.ImageUrl = model.ImageUrl;
                var shop = _shopRepository.GetByFk(user.Id);
                post.ShopId = shop.Id;
                postRepository.Add(post);
                return RedirectToAction("UserProfile", "Shop", new { UserName = user. UserName});
                }
            return RedirectToAction("UserProfile", "Shop", new { Username = user.UserName });
            }
        public IActionResult Creategalery()
            {
            GalleryViewModel model = new GalleryViewModel();
            return PartialView("GalleryPartialView", model);
            }
        [HttpPost]
        public async Task<IActionResult> Creategalery( GalleryViewModel model )
            {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
                {
                string uniqueFileName = null;
                if (model.ImageFile != null)
                    {
                    string uniqueUpload = Path.Combine(webHostEnvironment.WebRootPath, "GalleryImage");
                    uniqueFileName = Guid.NewGuid().ToString() + "" + model.ImageFile.FileName;
                    string filePath = Path.Combine(uniqueUpload, uniqueFileName);
                    await model.ImageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    model.ImageUrl = uniqueFileName;
                    }

                //var user = await _userManager.GetUserAsync(User);
                //  var category = categoryRepository.GetByCategoryName(model.CategoryName);
                // var shop = _shopRepository.Get(user.Id);
                Gallery post = new Gallery();
                post.Name = model.Name;
                post.Description = model.Description;
                post.ImageUrl = model.ImageUrl;
                var shop = _shopRepository.GetByFk(user.Id);
                post.ShopId = shop.Id;
                galleryRepository.Add(post);
                return RedirectToAction("UserProfile", "Shop", new { UserName = user.UserName });
                }
            return RedirectToAction("UserProfile","Shop",new {Username=user.UserName});
            }
        public async Task<IActionResult> DeleteImage( int id )
            {
            galleryRepository.Delete(id);
            var user = await _userManager.GetUserAsync(User);
            return RedirectToAction("UserProfile", "Shop", new { UserName = user.UserName });
            }
        public async Task<IActionResult> CreateRequest()
            {
              var user = await _userManager.GetUserAsync(User);
              var shop = _shopRepository.GetByFk(user.Id);
                shop.RequestStatus = RequestStatus.Pending;
                _shopRepository.Update(shop);
                return RedirectToAction("GeneralShopProfile","Shop");
            }
        }
    }
