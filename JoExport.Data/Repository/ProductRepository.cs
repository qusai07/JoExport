
using JoExport.Data.EfCore;
using JoExport.Domain.Model;
using System.Linq.Expressions;

namespace JoExport.Data.Repository;

        public class ProductRepository:IProductRepository
        {
    private readonly AppDbContext appDbContext;
        public ProductRepository( AppDbContext appDbContext )
        {
        this.appDbContext = appDbContext;
        }

    public Product Add( Product entity )
        {
        appDbContext.products.Add(entity);
        appDbContext.SaveChanges();
        return entity;
        }

    public Product Delete( object id )
        {
        var user = appDbContext.products.Find(id);
        if (user != null)
            {
            appDbContext.products.Remove(user);
            appDbContext.SaveChanges();
            }
        return user;
        }

    public Product Find( Expression<Func<Product, bool>> predicate )
        {
        return appDbContext.products.FirstOrDefault(predicate);
        }

    public IEnumerable<Product> Finds( Expression<Func<Product, bool>> predicate )
        {
        return appDbContext.products.Where(predicate);
        }

    public IEnumerable<Product> GetAll()
        {
        return appDbContext.products;
        }

    public Product Update( Product entity )
        {
        appDbContext.products.Update(entity);
        appDbContext.SaveChanges();
        return entity;
        }
    }
public interface IProductRepository:IRepository<Product>
    {
   
    }
