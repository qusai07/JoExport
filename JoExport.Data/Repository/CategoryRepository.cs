using JoExport.Data.EfCore;
using JoExport.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace JoExport.Data.Repository;

public class CategoryRepository : ICategoryRepository
    {
    private readonly AppDbContext appDbContext;
    public CategoryRepository( AppDbContext appDbContext )
        {
        this.appDbContext = appDbContext;
        }

    public Category Add( Category entity )
        {
        appDbContext.Categories.Add(entity);
        appDbContext.SaveChanges();
        return entity;
        }

    public Category Delete( object id )
        {
        var user = appDbContext.Categories.Find(id);
        if (user != null)
            {
            appDbContext.Categories.Remove(user);
            appDbContext.SaveChanges();
            }
        return user;
        }

    public Category Find( Expression<Func<Category, bool>> predicate )
        {
        return appDbContext.Categories.FirstOrDefault(predicate);
        }

    public IEnumerable<Category> Finds( Expression<Func<Category, bool>> predicate )
        {
        return appDbContext.Categories.Where(predicate);
        }

    public IEnumerable<Category> GetAll()
        {
        return appDbContext.Categories;
        }

    public Category Update( Category entity )
        {
        appDbContext.Categories.Update(entity);
        appDbContext.SaveChanges();
        return entity;
        }
    }
    public interface ICategoryRepository : IRepository<Category> { }