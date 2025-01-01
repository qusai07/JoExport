using Microsoft.AspNetCore.Identity;
using ProjectFutureAdvannced.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFutureAdvannced.Models.Model.AccountUser
{
    public class Admin:Account
    {
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public AppUser IdentityUser { get; set; }
        }
}
