using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Data;
using ProjectFutureAdvannced.Models.IRepository;
using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using ProjectFutureAdvannced.ViewModels.UserViewModel;
using System.Linq;
using System.Linq.Expressions;

namespace ProjectFutureAdvannced.Models.SqlRepository
    {
    public class WishlistRepository : IWishlistRRepository
        {
        public readonly AppDbContext _appDbContext;
        public WishlistRepository( AppDbContext _appDbContext )
            {
            this._appDbContext = _appDbContext;
            }
        public Wishlist Add( Wishlist card )
            {
            _appDbContext.Wishlists.Add(card);
            _appDbContext.SaveChanges();
            return card;
            }
        public IEnumerable<Product> GetAllProductByUserId( int userId )
            {
            return _appDbContext.Wishlists
                .Where(card => card.UserId == userId)
                .Select(card => card.Product)
                .ToList();
            }
        public async Task<IEnumerable<Wishlist>> DeleteAllWishListByProductId( int ProductId )
            {
            var wishlistsToDelete =await _appDbContext.Wishlists.Where(e => e.ProductId == ProductId).ToListAsync(); // Execute the query and fetch the records to be deleted
            if (wishlistsToDelete.Any())
                {
                _appDbContext.Wishlists.RemoveRange(wishlistsToDelete);
                await _appDbContext.SaveChangesAsync();
                }
            return wishlistsToDelete;
            }
        public Wishlist Delete( int Userid, int Productid )
            {
            var wishlist = _appDbContext.Wishlists.Find(Userid, Productid);
            if (wishlist != null)
                {
                _appDbContext.Wishlists.Remove(wishlist);
                _appDbContext.SaveChanges();

                }
            return wishlist;
            }
        }
    }
