using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoExport.Domain.Model.AccountUser
    {
    public class AppUser:IdentityUser
        {
        public string? ImgUrl { get; set; }

        }
    }
