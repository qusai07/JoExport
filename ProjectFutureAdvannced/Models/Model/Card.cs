using ProjectFutureAdvannced.Models.Model.AccountUser;
using System.ComponentModel.DataAnnotations;

namespace ProjectFutureAdvannced.Models.Model
    {
    public class Card
        {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        }
    }
