using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JoExport.Domain.Model.AccountUser
    {
    public class Account
        {
            public int Id { get; set; }
            [Required]
            [MaxLength(20)]
            public string Name { get; set; }
            [MaxLength(20)]
            public string? Major { get; set; }
            [RegularExpression(@"^(?i)(male|female)$")]
            public string? Gender { get; set; }
            [RegularExpression(@"(77|79|78)\d{7}")]
            public string? PhoneNumber { get; set; }
        }
    }
