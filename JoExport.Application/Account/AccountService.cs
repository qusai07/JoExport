using JoExport.Data.Repository;
using JoExport.Domain.Model.AccountUser;
using JoExport.Domain.Model.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectFutureAdvannced.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoExport.Application.Account;
internal class AccountService : IAccountService
    {
    private readonly Data.Repository.IAdminRepository _adminRepository;
    private readonly Data.Repository.IUserRepository _userRepository;
    private readonly Data.Repository.IShopRepository _shopRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountService( Data.Repository.IAdminRepository _adminRepository, Data.Repository.IUserRepository _userRepository, Data.Repository.IShopRepository _shopRepository, UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager )
        {
        this._adminRepository = _adminRepository;
        this._userRepository = _userRepository;
        this._shopRepository = _shopRepository;
        this._userManager = _userManager;
        this._signInManager = _signInManager;
        this._roleManager = _roleManager;
        }
    public async Task<bool> CreateAdmin( RegisterAdminViewModel model )
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
            await _userManager.AddToRoleAsync(userr, "Admin");
            Domain.Model.AccountUser.Admin admin = new Domain.Model.AccountUser.Admin()
                {
                Name = model.Name,
                UserId = userr.Id,
                };

            _adminRepository.Add(admin);
            await _signInManager.SignInAsync(userr, isPersistent: false);
            return true;
            }
        return false;
        }
    public async Task<bool> Create( RegisterViewModel model )
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
            //if (model.TypeOfRoles == TypeOfUser.Shop)
                {
                if (await _roleManager.RoleExistsAsync(TypeOfUser.Shop.ToString()))
                    {
                    /**************************/
                    await _userManager.AddToRoleAsync(userr, "Shop");
                    Domain.Model.AccountUser.Shop shop = new Domain.Model.AccountUser.Shop
                        {
                        Name = model.Name,
                        UserId = userr.Id,
                        };
                    _shopRepository.Add(shop);
                    /**************************/
                    }
                }
            // else
            // if (model.TypeOfRoles == TypeOfUser.User)
                {
                if (await _roleManager.RoleExistsAsync("User"))
                    {
                    /******************************/
                    await _userManager.AddToRoleAsync(userr, "User");
                    Domain.Model.AccountUser.User user = new Domain.Model.AccountUser.User
                        {
                        Name = model.Name,
                        UserId = userr.Id,

                        };
                    _userRepository.Add(user);
                    /*******************************/
                    }
                }
            await _signInManager.SignInAsync(userr, isPersistent: false);
            return true;
            }
        return false;
        }

    public async Task<bool> Login( LoginViewModel model )
        {
        } 
    }
public interface IAccountService
    {
    Task<bool> CreateAdmin( RegisterAdminViewModel model );
    Task<bool> Create( RegisterViewModel model );
    Task<bool> Login( LoginViewModel model,out string account );
    }
