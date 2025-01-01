using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;

namespace ProjectFutureAdvannced.ViewModels.UserViewModel
    {
    public class ListOfInfoUser
        {
        public IEnumerable<Product> Products { get; set; }
        public Card Card { get; set; }
        public Product Product  { get; set; }

        public IEnumerable<Card> cards { get; set; }
        public Account carrentUser { get; set; }
        }
    }
