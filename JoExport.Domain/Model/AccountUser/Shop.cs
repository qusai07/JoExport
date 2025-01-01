using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoExport.Domain.Model.Enums;

namespace JoExport.Domain.Model.AccountUser
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
        }
    }
