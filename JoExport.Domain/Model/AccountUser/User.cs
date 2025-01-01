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
    public class User : Account
        {
        [Required]
        public TypeOfUser TypeOfRoles { get; set; }
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public AppUser IdentityUser { get; set; }
        public ICollection<Product> Products { get; set; }
        public List<Card> cards { get; set; }
        }
    }
