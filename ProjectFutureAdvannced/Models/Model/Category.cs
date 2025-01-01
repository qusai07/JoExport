using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Models.Enums;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectFutureAdvannced.Models.Model
    {
    public class Category
        {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"\b\w+\.(jpg|JPG|PNG|png)\b", ErrorMessage = "This Image not supported")]
        public string CategoryImg { get; set; }
       //public Shop shop { get; set; }
       //public List<Product> Products { get; set; }
        }
    }
