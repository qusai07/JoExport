using JoExport.Domain.Model.AccountUser;
using JoExport.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoExport.Domain.Model
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

        public Categorys CategoryName { get; set; }
        [Required]

        public int ShopId { get; set; }
        public Shop shop { get; set; }
        public ICollection<User> users { get; set; }
        public List<Card> cards { get; set; }
        }
    }
