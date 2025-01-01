using ProjectFutureAdvannced.Models.Model.AccountUser;

namespace ProjectFutureAdvannced.Models.IRepository
{
    public interface IUserRepository
        {
        public User Add( User user );
        public User Delete( string Id );
        public User Get(int id );
        public IEnumerable<User> GetAll();
        public User Update( User user );
        public User GetByFk( string Fk );

        }
    }
