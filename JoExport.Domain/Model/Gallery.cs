using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoExport.Domain.Model
    {
    public class Gallery
        {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$")]
        public string ImageUrl { get; set; }
        public int ShopId { get; set; }
        public Shop shop { get; set; }
        }
    }
