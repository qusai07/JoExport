using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoExport.Domain.Model.AccountUser
    {
    public class Admin:Account
        {
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public AppUser IdentityUser { get; set; }
        }
    }
