using Microsoft.AspNetCore.Identity;
using ProjectFutureAdvannced.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFutureAdvannced.Models.Model.AccountUser
{
    public class Shop:Account
    {

        [Required]
        public TypeOfUser TypeOfRoles { get; set; }
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public AppUser IdentityUser { get; set; }
        //public Categorys ?CategoryName { get; set; }
        //public Category Category { get; set; }
        public List<Product> Products { get; set; }
        public List<Gallery> galleries { get; set; }
        public List<Post> posts { get; set; }
        public RequestStatus? RequestStatus { get; set; }
        }
    }
