using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectFutureAdvannced.Models.IRepository;
using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using ProjectFutureAdvannced.Models.SqlRepository;
using ProjectFutureAdvannced.ViewModels;
using ProjectFutureAdvannced.ViewModels.AdminViewModel;
using ProjectFutureAdvannced.ViewModels.UserViewModel;
using System.Net;

namespace ProjectFutureAdvannced.Controllers
    {
    public class UserController : Controller
        {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductRepository productRepository;
        private readonly IShopRepository shopRepository;
        private readonly ICartRepository cartRepository;
        private readonly IWishlistRRepository _wishlistRRepository;


        public UserController( IWishlistRRepository _wishlistRRepository, ICartRepository cartRepository, IShopRepository shopRepository, IProductRepository productRepository, IWebHostEnvironment webHostEnvironment, IUserRepository userRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> _roleManager )
            {
            _userManager = userManager;
            _signInManager = signInManager;
            this._roleManager = _roleManager;
            this.userRepository = userRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.productRepository = productRepository;
            this.cartRepository= cartRepository;
            this._wishlistRRepository = _wishlistRRepository;
            }
        [HttpGet]
        public async Task<IActionResult> GeneralUserProfile()
            {
            GeneralInfoUser model;
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
                {
                var Shop = userRepository.GetByFk(user.Id);

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
        public async Task<IActionResult> GeneralUserProfile( GeneralInfoUser model )
            {
            if (ModelState.IsValid)
                {
                string uniqueFileName = null;
                var user = await _userManager.GetUserAsync(User);
                var userr = userRepository.GetByFk(user.Id);
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
                    if ((user.ImgUrl != null && model.Gender != null) || (user.ImgUrl == null && model.Gender != null))
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
                    if ((user.ImgUrl != null && model.Gender == null))
                        {
                        uniqueFileName = user.ImgUrl;
                        }
                    model.UrlImgString = uniqueFileName;
                    }
                /*********************/
                int indexOfAt = model.Email.IndexOf("@");
                userr.Name = model.Name;
                user.ImgUrl = model.UrlImgString;
                userr.UserName = model.Email.Substring(0, indexOfAt);
                userr.Email = model.Email;
                /*************************/
                userr.Gender = model.Gender;
                userr.PhoneNumber = model.PhoneNumber;
                userr.Major = model.Major;
                userRepository.Update(userr);
                await _userManager.SetEmailAsync(user, user.Email);
                await _userManager.SetUserNameAsync(user, model.Email.Substring(0, indexOfAt));
                await _userManager.UpdateAsync(user);
                return RedirectToAction("GeneralUserProfile", "User");
                }
            return View();
            }
        /***********************************/
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
                                return RedirectToAction("SecurityUser", "User");
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
        public async Task<IActionResult> listCart()
            {
            var user = await _userManager.GetUserAsync(User);
            var UserAccount = userRepository.GetByFk(user.Id);
            ListOfInfoUser listOfInfoUser = new ListOfInfoUser
                {
                Products = cartRepository.GetAllProductByUserId(UserAccount.Id),
                };
            return View(listOfInfoUser);
            }
        public async Task<IActionResult> Card( int id, int productId )
            {
            var user = await _userManager.GetUserAsync(User);
            //var cartt = cardRepository.GetCard(id);
            //if (cartt == null)
            //    {
            //    //Card card = new Card()
            //    //    {
            //    //    UserId = user.Id,
            //    //    ProductId=productId,
            //    //    };
            //    //cardRepository.Add(card);
            //    }
            return RedirectToAction("Index", "Home");
            }
        public async Task<IActionResult> MyWishlist( int id )
            {
            var userIdentity = await _userManager.GetUserAsync(User);
            var user = userRepository.GetByFk(userIdentity.Id);
            var Products = _wishlistRRepository.GetAllProductByUserId(user.Id);
            return View(Products);
            }
        /***************************/
        public async Task<IActionResult> AddWishList( int id )
            {
            var product = productRepository.GetById(id);

            if (product == null)
                {
                // Handle case when the product doesn't exist
                return NotFound();
                }

            var userIdentity = await _userManager.GetUserAsync(User);

            if (userIdentity == null)
                {
                // Handle case when the user is not authenticated
                return Unauthorized();
                }

            var user = userRepository.GetByFk(userIdentity.Id);

            if (user == null)
                {
                // Handle case when the user doesn't exist
                return NotFound();
                }

            Wishlist wishlist = new Wishlist
                {
                ProductId = id,
                UserId = user.Id,
                };

            _wishlistRRepository.Add(wishlist);
            return RedirectToAction("Store", "Home");
            }
        /***************************/

        public async Task<IActionResult> AddCart( int id )
            {
            var product = productRepository.GetById(id);

            if (product == null)
                {
                // Handle case when the product doesn't exist
                return NotFound();
                }

            var userIdentity = await _userManager.GetUserAsync(User);

            if (userIdentity == null)
                {
                // Handle case when the user is not authenticated
                return Unauthorized();
                }

            var user = userRepository.GetByFk(userIdentity.Id);

            if (user == null)
                {
                // Handle case when the user doesn't exist
                return NotFound();
                }

            Card card = new Card
                {
                ProductId = id,
                UserId = user.Id,
                };

            cartRepository.Add(card);
            return RedirectToAction("Store", "Home");
            }
        public IActionResult ReomveCart( int Userid ,int Productid)
            {
             cartRepository.Delete(Userid, Productid);
            return RedirectToAction("listCart", "User");
            }
        public IActionResult ReomveWishlist( int Userid, int Productid )
            {
            _wishlistRRepository.Delete(Userid, Productid);
            return RedirectToAction("MyWishlist", "User");
            }
        }
    }
