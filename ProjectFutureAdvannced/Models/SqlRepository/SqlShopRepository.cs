using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Data;
using ProjectFutureAdvannced.Models.IRepository;
using ProjectFutureAdvannced.Models.Model.AccountUser;

namespace ProjectFutureAdvannced.Models.SqlRepository
{
    public class SqlShopRepository : IShopRepository
        {
        private readonly AppDbContext appDbContext;
        public SqlShopRepository( AppDbContext appDbContext )
            {
            this.appDbContext = appDbContext;
            }
        public Shop Add( Shop Shop )
            {
            appDbContext.Shop.Add(Shop);
            appDbContext.SaveChanges();
            return Shop;
            }

        public Shop Delete( string id )
            {
            var Shop = appDbContext.Shop.FirstOrDefault(e => e.UserId == id);
            if (Shop != null)
                {
                appDbContext.Shop.Remove(Shop);
                appDbContext.SaveChanges();
                }
            return Shop;
            }

        public Shop Get( int id )
            {
            return appDbContext.Shop.Find(id);
            }

        public IEnumerable<Shop> GetAll()
            {
            return appDbContext.Shop;
            }

        public Shop GetByFk( string Fk )
            {
            return appDbContext.Shop.FirstOrDefault(e => e.UserId == Fk);
            }

        public Shop Update( Shop Shop )
            {
            var Shops = appDbContext.Shop.Attach(Shop);
            Shops.State = EntityState.Modified;
            appDbContext.SaveChanges();
            return Shop;
            }
        }
    }
