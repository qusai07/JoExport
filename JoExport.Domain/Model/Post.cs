using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoExport.Domain.Model
    {
    public class Post
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string ImageUrl { get; set; }
        public int ShopId { get; set; }
        public Shop shop { get; set; }
        }
    }
