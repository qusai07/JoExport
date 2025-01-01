using Microsoft.AspNetCore.Identity;
using ProjectFutureAdvannced.Models.Enums;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFutureAdvannced.Models.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public double Price { get; set; }
        [Required]

        public string Image { get; set; }
        /////***********************************/
        [Required]

        public string CategoryName { get; set; }
        [Required]

        public int ShopId { get; set; }
        public Shop shop { get; set; }
        public ICollection<User> users { get; set; }
        public List<Card> cards { get; set; }
        public List<Wishlist> Wishlists { get; set; }

        }

    }
