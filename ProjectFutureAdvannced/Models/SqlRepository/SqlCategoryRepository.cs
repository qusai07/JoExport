using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectFutureAdvannced.Data;
using ProjectFutureAdvannced.Models.Enums;
using ProjectFutureAdvannced.Models.IRepository;
using ProjectFutureAdvannced.Models.Model;

namespace ProjectFutureAdvannced.Models.SqlRepository
{
    public class SqlCategoryRepository : ICategoryRepository
        {
        private readonly AppDbContext appDbContext;
        public SqlCategoryRepository( AppDbContext appDbContext )
        {
            this.appDbContext = appDbContext;
        }
        public Category Add( Category category )
            {
            appDbContext.Categories.Add( category );
            appDbContext.SaveChanges();
            return category;    
            }

        public Category Delete( int id )
            {
            var category = appDbContext.Categories.Find(id);
            if( category != null )
                {
                appDbContext.Categories.Remove( category );
                appDbContext.SaveChanges();
                }
            return category;
            }

        public Category Get( int id )
            {
            var category= appDbContext.Categories.Find(id);
            return category;
            }

        public IEnumerable<Category> GetAll()
            {
            return appDbContext.Categories;
            }

        public Category GetByCategoryName( int category )
            {
            throw new NotImplementedException();
            }
        public Category GetByCategoryName( string categorys )
            {
            return appDbContext.Categories.FirstOrDefault(e => e.Name == categorys);
            }

        public Category Update( Category category )
            {
            var categorys=appDbContext.Categories.Attach(category);
            categorys.State= EntityState.Modified;
            appDbContext.SaveChanges();
            return category;
            }
        }
    }
