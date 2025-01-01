using JoExport.Data.EfCore;
using JoExport.Domain.Model.AccountUser;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JoExport.Data.Repository;
    public class ShopRepository:IShopRepository
        {
    private readonly AppDbContext appDbContext;
    public ShopRepository( AppDbContext appDbContext )
        {
        this.appDbContext = appDbContext;
        }

    public Shop Add( Shop entity )
        {
        appDbContext.Shop.Add(entity);
        appDbContext.SaveChanges();
        return entity;
        }

    public Shop Delete( object id )
        {
        var user = appDbContext.Shop.Find(id);
        if (user != null)
            {
            appDbContext.Shop.Remove(user);
            appDbContext.SaveChanges();
            }
        return user;
        }

    public Shop Find( Expression<Func<Shop, bool>> predicate )
        {
        return appDbContext.Shop.FirstOrDefault(predicate);
        }

    public IEnumerable<Shop> Finds( Expression<Func<Shop, bool>> predicate )
        {
        return appDbContext.Shop.Where(predicate);
        }

    public IEnumerable<Shop> GetAll()
        {
        return appDbContext.Shop;
        }

    public Shop Update( Shop entity )
        {
        appDbContext.Shop.Update(entity);
        appDbContext.SaveChanges();
        return entity;
        }
    }
public interface IShopRepository:IRepository<Shop>
    {
   
    }