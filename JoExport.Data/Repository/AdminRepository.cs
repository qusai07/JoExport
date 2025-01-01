using JoExport.Data.EfCore;
using JoExport.Domain.Model.AccountUser;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace JoExport.Data.Repository;
    public class AdminRepository:IAdminRepository
        {
    private readonly AppDbContext appDbContext;
    public AdminRepository( AppDbContext appDbContext )
        {
        this.appDbContext = appDbContext;
        }

    public Admin Add( Admin entity )
        {
        appDbContext.Admin.Add(entity);
        appDbContext.SaveChanges();
        return entity;
        }
    public Admin Delete( object id )
        {
        var user = appDbContext.Admin.Find(id);
        if (user != null)
            {
            appDbContext.Admin.Remove(user);
            appDbContext.SaveChanges();
            }
        return user;
        }

    public Admin Find( Expression<Func<Admin, bool>> predicate )
        {
        return appDbContext.Admin.FirstOrDefault(predicate);
        }

    public IEnumerable<Admin> Finds( Expression<Func<Admin, bool>> predicate )
        {
        return appDbContext.Admin.Where(predicate);
        }

    public IEnumerable<Admin> GetAll()
        {
        return appDbContext.Admin;
        }
    public Admin Update( Admin entity )
        {
        appDbContext.Admin.Update(entity);
        appDbContext.SaveChanges();
        return entity;
        }
    }
public interface IAdminRepository:IRepository<Admin>
    {

    }
