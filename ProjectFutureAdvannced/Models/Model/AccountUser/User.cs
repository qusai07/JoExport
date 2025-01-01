using Microsoft.AspNetCore.Identity;
using ProjectFutureAdvannced.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFutureAdvannced.Models.Model.AccountUser
{
    public class User:Account
        {
     

        [Required]
        public TypeOfUser TypeOfRoles { get; set; }
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public AppUser IdentityUser { get; set; }
        public ICollection<Product> Products { get; set; }
        public List<Card> cards { get; set; }
        public List<Wishlist> Wishlists { get; set; }
        }
    }
