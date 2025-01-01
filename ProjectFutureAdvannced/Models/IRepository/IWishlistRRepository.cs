using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using System.Linq.Expressions;

namespace ProjectFutureAdvannced.Models.IRepository
    {
    public interface IWishlistRRepository
        {
        public Wishlist Add( Wishlist wishlist );
        public  Task<IEnumerable<Wishlist>> DeleteAllWishListByProductId( int ProductId );
        public IEnumerable<Product> GetAllProductByUserId( int userId );
        public Wishlist Delete( int Userid, int Productid );
        }
    }
