using ProjectFutureAdvannced.Models.Model.AccountUser;

namespace ProjectFutureAdvannced.Models.IRepository
{
    public interface IShopRepository
        {
        public Shop Add(Shop shop);
        public Shop Delete( string Id );
        public Shop Get( int id );
        public IEnumerable<Shop> GetAll();
        public Shop Update( Shop shop );
        public Shop GetByFk( string Fk );

        }
    }
