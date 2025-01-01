using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectFutureAdvannced.Models.Enums;
using ProjectFutureAdvannced.Models.IRepository;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using ProjectFutureAdvannced.ViewModels;

namespace ProjectFutureAdvannced.Controllers
    {
    public class AccountController : Controller
        {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAdminRepository adminRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController( IShopRepository _shopRepository, IUserRepository userRepository,IWebHostEnvironment webHostEnvironment, IAdminRepository adminRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> _roleManager )
            {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = _roleManager;
            this.adminRepository = adminRepository;
            this.webHostEnvironment = webHostEnvironment;
            this._shopRepository = _shopRepository;
            this.userRepository = userRepository;
            }

        public IActionResult Index()
            {

            return View();
            }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create()
            {
            return View();
            }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> CreateAdmin()
            {
            return View();
            }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateAdmin( RegisterAdminViewModel model )
            {
            if (ModelState.IsValid)
                {
                int indexOfAt = model.Email.IndexOf("@");
                AppUser userr = new AppUser()
                    {
                    Email = model.Email,
                    UserName = model.Email.Substring(0, indexOfAt),
                    ImgUrl = "img_avatar.png",
                    };
                var result = await _userManager.CreateAsync(userr, model.Password);
                if (result.Succeeded)
                    {
                    /**************************/
                            await _userManager.AddToRoleAsync(userr, "Admin");
                    Admin Admin = new Admin
                        {
                        Name = model.Name,
                        Email = model.Email,
                        UserName = model.Email.Substring(0, indexOfAt),
                        Password = model.Password,
                        ConfirmPassword = model.ConfirmPassword,
                        UserId = userr.Id,
                                };
                             /**************************/
                            adminRepository.Add(Admin);
                        await _signInManager.SignInAsync(userr, isPersistent: false);
                        return RedirectToAction("Index", "Admin");
                        }
                    }
            return View();
            }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create( RegisterViewModel model )
            {
            if (ModelState.IsValid)
                {
                int indexOfAt = model.Email.IndexOf("@");
                AppUser userr = new AppUser()
                    {
                    Email = model.Email,
                    UserName = model.Email.Substring(0, indexOfAt),
                    ImgUrl = "img_avatar.png",
                    };
                var result = await _userManager.CreateAsync(userr, model.Password);
                if (result.Succeeded)
                    {
                    if (model.TypeOfRoles == TypeOfUser.Shop)
                        {
                        if (await _roleManager.RoleExistsAsync(TypeOfUser.Shop.ToString()))
                            {
                            /**************************/
                            await _userManager.AddToRoleAsync(userr, "Shop");
                            Shop shop = new Shop
                                {
                                Name = model.Name,
                                Email = model.Email,
                                UserName = model.Email.Substring(0, indexOfAt),
                                Password=model.Password,
                                ConfirmPassword = model.ConfirmPassword,
                                UserId = userr.Id,

                                };
                            _shopRepository.Add(shop);
                            /**************************/
                            }
                        }
                    else
                    if (model.TypeOfRoles == TypeOfUser.User)
                        {
                        if (await _roleManager.RoleExistsAsync("User"))
                            {
                            /******************************/
                            await _userManager.AddToRoleAsync(userr, "User");
                            User user = new User
                                {
                                Name = model.Name,
                                Email = model.Email,
                                UserName = model.Email.Substring(0, indexOfAt),
                                Password = model.Password,
                                ConfirmPassword = model.ConfirmPassword,
                                UserId = userr.Id,

                                };
                            userRepository.Add(user);
                            /*******************************/
                            }
                        }
                    await _signInManager.SignInAsync(userr,isPersistent:false);
                    return RedirectToAction("Index","Home");
                    }
                }
            return View();
            }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
            {
            return View();
            }
        [AllowAnonymous]

        [HttpPost]
        public async Task<IActionResult> Login( LoginViewModel model, string returnUrl = null )
            {
            if (ModelState.IsValid)
                {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                    if (model.RemmberMe)
                        {
                        await _signInManager.SignInAsync(user, isPersistent: true);
                        }
                    else
                        {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        }
                        if (await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                        return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                        if (Url.IsLocalUrl(returnUrl) && !String.IsNullOrEmpty(returnUrl))
                            {
                            return LocalRedirect(returnUrl);
                            }
                        }
                    return RedirectToAction("Index", "Home");
                    }
                }
            return View(model);
            }
        [Authorize]
        public async Task<IActionResult> Logout()
            {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
            }
        [Authorize]
        public async Task<IActionResult> Remove()
            {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
                {
                if (await _roleManager.RoleExistsAsync("Admin"))
                    {

                    adminRepository.Delete(user.Id);
                    }
                else
              if (await _roleManager.RoleExistsAsync("Shop"))
                    {
                    _shopRepository.Delete(user.Id);
                    }
                else
              if (await _roleManager.RoleExistsAsync("User"))
                    {
                    userRepository.Delete(user.Id);
                    }
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                    {
                    // Account deleted successfully
                    return RedirectToAction("Index", "Home");
                    }
                else
                    {
                    // Handle errors if account deletion fails
                    foreach (var error in result.Errors)
                        {
                        ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            return View();
            }
        //[Authorize]
        //public async Task<IActionResult> UserProfile(string UserName )
        //    {
        //    var user = await _userManager.FindByNameAsync(UserName);
        //    ListOfInfo listOfInfo = new ListOfInfo()
        //        {
        //        AppUser = user,
        //        };
        //    return View(listOfInfo);
        //    }
        }
    }
