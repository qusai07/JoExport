using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Data;
using ProjectFutureAdvannced.Models.IRepository;
using ProjectFutureAdvannced.Models.Model.AccountUser;

namespace ProjectFutureAdvannced.Models.SqlRepository
{
    public class SqlUserRepository:IUserRepository
        {
        private readonly AppDbContext appDbContext;
        public SqlUserRepository( AppDbContext appDbContext )
            {
            this.appDbContext = appDbContext;
            }
        public User Add( User user )
            {
            appDbContext.User.Add(user);
            appDbContext.SaveChanges();
            return user;
            }

        public User Delete( string id )
            {
            var Users = appDbContext.User.FirstOrDefault(e => e.UserId == id);
            if (Users != null)
                {
                appDbContext.User.Remove(Users);
                appDbContext.SaveChanges();
                }
            return Users;
            }
        public User Get( int id )
            {
            return appDbContext.User.Find(id);
            }



        public IEnumerable<User> GetAll()
            {
            return appDbContext.User;
            }

        public User GetByFk( string Fk )
            {
            return appDbContext.User.FirstOrDefault(e => e.UserId == Fk);
            }

        public User Update( User User )
            {
            var Users = appDbContext.User.Attach(User);
            Users.State = EntityState.Modified;
            appDbContext.SaveChanges();
            return User;
            }
        }
    }
