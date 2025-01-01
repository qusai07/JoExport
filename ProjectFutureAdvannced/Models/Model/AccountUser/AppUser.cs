using Microsoft.AspNetCore.Identity;
using ProjectFutureAdvannced.Models.Enums;

namespace ProjectFutureAdvannced.Models.Model.AccountUser
    {
    public class AppUser:IdentityUser
        {
        public string? ImgUrl { get; set; }
        }
    }
