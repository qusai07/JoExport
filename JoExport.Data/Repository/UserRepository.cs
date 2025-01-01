using JoExport.Data.EfCore;
using JoExport.Data.Repository;
using JoExport.Domain.Model.AccountUser;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JoExport.Data.Repository;
    public class UserRepository:IUserRepository
        {
    private readonly AppDbContext appDbContext;
    public UserRepository( AppDbContext appDbContext )
        {
        this.appDbContext = appDbContext;
        }

    public User Add( User entity )
        {
        appDbContext.User.Add(entity);
        appDbContext.SaveChanges();
        return entity;
        }

    public User Delete( object id )
        {
        var user = appDbContext.User.Find(id);
        if (user != null)
            {
            appDbContext.User.Remove(user);
            appDbContext.SaveChanges();
            }
        return user;
        }

    public User Find( Expression<Func<User, bool>> predicate )
        {
        return appDbContext.User.FirstOrDefault(predicate);
        }

    public IEnumerable<User> Finds( Expression<Func<User, bool>> predicate )
        {
        return appDbContext.User.Where(predicate);
        }

    public IEnumerable<User> GetAll()
        {
        return appDbContext.User;
        }

    public User Update( User entity )
        {
        appDbContext.User.Update(entity);
        appDbContext.SaveChanges();
        return entity;
        }
    }

    public interface IUserRepository:IRepository<User>
    {
    }