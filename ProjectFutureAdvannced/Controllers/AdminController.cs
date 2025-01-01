using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Models.Enums;
using ProjectFutureAdvannced.Models.IRepository;
using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using ProjectFutureAdvannced.Models.SqlRepository;
using ProjectFutureAdvannced.ViewModels;
using ProjectFutureAdvannced.ViewModels.AdminViewModel;
using ProjectFutureAdvannced.ViewModels.CategoryViewModel;
using ProjectFutureAdvannced.ViewModels.ProductViewModel;
using System.Runtime.InteropServices;

namespace ProjectFutureAdvannced.Controllers
    {
    public class AdminController : Controller
        {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAdminRepository adminRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;
        private readonly ICartRepository cartRepository;
        private readonly IShopRepository shopRepository;

        public AdminController( IShopRepository shopRepository, ICartRepository cartRepository, IUserRepository userRepository, IProductRepository productRepository, IWebHostEnvironment webHostEnvironment, IAdminRepository adminRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> _roleManager,ICategoryRepository categoryRepository )
            {
            this.shopRepository=shopRepository;
            this.cartRepository= cartRepository;
            this.userRepository= userRepository;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = _roleManager;
            this.adminRepository = adminRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var users = await _userManager.Users.ToListAsync();


            return View(users);
        }
        public async Task<IActionResult> tableAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            ListOfInfoAdmin listOfInfoAdmin = new ListOfInfoAdmin();
            listOfInfoAdmin.appUsers = users;
            return View(users);
        }
        public async Task<IActionResult> category()
        {
            var categories = categoryRepository.GetAll();
            var users = await _userManager.Users.ToListAsync();
            ListOfInfo listOfInfo = new ListOfInfo()
                {
                categories = categories,
                appUsers= users,
            };
            return View(listOfInfo);
        }

        [HttpGet]
        public async Task<IActionResult> GeneralProfile()
            {
            GeneralInfoAdmin model;
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
                {
                var admin = adminRepository.GetByFk(user.Id);
                
                model = new GeneralInfoAdmin()
                    {
                    Name = admin.Name,
                    Email = admin.Email,
                    UrlImgString = user.ImgUrl,
                    PhoneNumber = admin.PhoneNumber,
                    Gender = admin.Gender,
                    Major=admin.Major,
                    };
                return View(model);
                }
            return View();
            }
        /*************************************/
        [HttpPost]
        public async Task<IActionResult> GeneralProfile( GeneralInfoAdmin model )
            {
            if (ModelState.IsValid)
                {
                string uniqueFileName = null;
                var user = await _userManager.GetUserAsync(User);
                var admin = adminRepository.GetByFk(user.Id);
                if (model.ImgUser != null)
                    {
                    string uniqueUpload = Path.Combine(webHostEnvironment.WebRootPath, "AccountImg");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImgUser.FileName;
                    string filePath = Path.Combine(uniqueUpload, uniqueFileName);
                    await model.ImgUser.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    model.UrlImgString = uniqueFileName;
                    }
                else
                    {
                    if ((user.ImgUrl != null && admin.Gender != null) || (user.ImgUrl == null && admin.Gender != null))
                        {
                        if (model.Gender == "Male")
                            {
                            uniqueFileName = "img_avatar.png";
                            }
                        else
                          if (model.Gender == "Female")
                            {
                            uniqueFileName = "img_avatar2.png";
                            }
                        else
                            {
                            uniqueFileName = user.ImgUrl;
                            }
                        }
                    else
                    if ((user.ImgUrl != null && admin.Gender == null))
                        {
                        uniqueFileName = user.ImgUrl;
                        }
                    model.UrlImgString = uniqueFileName;
                    }

                /*********************/
                int indexOfAt = model.Email.IndexOf("@");
                admin.Name = model.Name;
                user.ImgUrl = model.UrlImgString;
                admin.UserName = model.Email.Substring(0, indexOfAt);
                admin.Email = model.Email;
                /*************************/
                admin.Name= model.Name;
                admin.Email= model.Email;
                admin.Gender=model.Gender;
                admin.PhoneNumber=model.PhoneNumber;
                admin.Major = model.Major;
                adminRepository.Update(admin);
                await _userManager.SetEmailAsync(user, user.Email);
                await _userManager.SetUserNameAsync(user, model.Email.Substring(0, indexOfAt));
                await _userManager.UpdateAsync(user);
                return RedirectToAction("GeneralProfile", "Admin");
                }
            return View();
            }
        /***********************************/
        [HttpGet]
        public async Task<IActionResult> SecurityAdmin()
            {
            var user=await _userManager.GetUserAsync(User);
            var admin = adminRepository.GetByFk(user.Id);
            SecurityAdmin security = new SecurityAdmin();
            return View(security);
            }
        [HttpPost]
        public async Task<IActionResult> SecurityAdmin(SecurityAdmin security)
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
                                return RedirectToAction("SecurityAdmin", "Admin");
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
        public  async Task<IActionResult> usersInfo(string UserId)
            {
            var user= userRepository.GetByFk(UserId);
            var carts = cartRepository.GetAllProductByUserId(user.Id);
            ListOfInfo listOfInfo = new ListOfInfo()
                {
                products = carts,
                AppUser =await _userManager.FindByIdAsync(UserId),
                };
            return View(listOfInfo);
            }

           public  async Task<IActionResult> ViewProduct(string ShopId)
            {
            var shop= shopRepository.GetByFk(ShopId);
            var products = productRepository.GetAllById(shop.Id);
            ListOfInfo listOfInfo = new ListOfInfo()
                {
                products = products,
                };
            return View(listOfInfo);
            }

        public async Task<IActionResult> reomveUser(string UserId )
            {
            var user = await _userManager.FindByIdAsync(UserId);
            var result = await _userManager.DeleteAsync(user);
             if(result.Succeeded)
                {
                return RedirectToAction("index", "Admin");
                }
            return View();
            }
        public IActionResult reomveCategory(int CategoryId )
            {
            categoryRepository.Delete(CategoryId);
            return RedirectToAction("category", "Admin");
            }
        [HttpGet]
        public IActionResult AddCategory()
            {
            return View();
            }

        [HttpPost]
        public async  Task<IActionResult>AddCategory(CreateCategoryViewModel model)
            {
            if (ModelState.IsValid)
                {
                string uniqueFileName = null;
                string uniqueUpload = Path.Combine(webHostEnvironment.WebRootPath, "CategoryImage");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uniqueUpload, uniqueFileName);
                await model.ImageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));
                model.UrlImgString = uniqueFileName;
                Category category = new Category()
                    {
                    CategoryImg = model.UrlImgString,
                    Name=model.Name,
                    };
                categoryRepository.Add(category);
                return RedirectToAction("Category", "Admin");
                }
            return View();
            }


        [HttpGet]
        public  IActionResult EditCategory( int id)
            {
            var category = categoryRepository.Get(id);
            EditCategoryViewModel model = new EditCategoryViewModel()
                {
                Name = category.Name,
                UrlImgString = category.CategoryImg,
                };
            return View(model);
            }

        [HttpPost]
        public async Task<IActionResult> EditCategory(int id, EditCategoryViewModel model )
            {
            if (ModelState.IsValid)
                {
                var CategoryToUpdate = categoryRepository.Get(id);

                if (CategoryToUpdate == null)
                    {
                    return NotFound();
                    }

                if (model.ImageFile != null)
                    {
                    if (!string.IsNullOrEmpty(CategoryToUpdate.CategoryImg))
                        {
                        var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, "CategoryImage", CategoryToUpdate.CategoryImg);
                        if (System.IO.File.Exists(oldImagePath))
                            {
                            System.IO.File.Delete(oldImagePath);
                            }
                        }
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "ProductImg", uniqueFileName);
                    await model.ImageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    CategoryToUpdate.CategoryImg = uniqueFileName;
                    }
                CategoryToUpdate.Name = model.Name;
                categoryRepository.Update(CategoryToUpdate);
                return RedirectToAction("Category", "Admin");
                }
            return View(model);
            }
        /***********************************/
        public async Task<IActionResult> Approved( string UserId )
            {
            var user = await _userManager.FindByIdAsync(UserId);
            var Shop = shopRepository.GetByFk(user.Id);
            Shop.RequestStatus = RequestStatus.Approved;
            shopRepository.Update(Shop);
            return RedirectToAction("Index", "Admin");
            }
        public async Task<IActionResult> Rejected( string UserId )
            {
            var user = await _userManager.FindByIdAsync(UserId);
            var Shop = shopRepository.GetByFk(user.Id);
            Shop.RequestStatus = RequestStatus.Rejected;
            shopRepository.Update(Shop);
            return RedirectToAction("Index", "Admin");
            }
        }
    }


