using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Data;
using ProjectFutureAdvannced.Models.Model;

namespace ProjectFutureAdvannced.Models.SqlRepository;
public class GalleryRepository : IGalleryRepository
    {
    private readonly AppDbContext appDbContext;
    public GalleryRepository( AppDbContext appDbContext )
        {
        this.appDbContext = appDbContext;
        }
    public Gallery Add( Gallery img )
        {
        appDbContext.galleries.Add(img);
        appDbContext.SaveChanges();
        return img;
        }

    public Gallery Delete( int id )
        {
        var Posts = appDbContext.galleries.Find(id);
        if (Posts != null)
            {
            appDbContext.galleries.Remove(Posts);
            appDbContext.SaveChanges();
            }
        return Posts;
        }

    public IEnumerable<Gallery> GetAll()
        {
        return appDbContext.galleries;
        }

    public IEnumerable<Gallery> GetPostByShopId( int id )
        {
        return appDbContext.galleries.Where(e => e.ShopId == id);
        }

    public Gallery Update( Gallery img )
        {
        var imgg = appDbContext.galleries.Attach(img);
        imgg.State = EntityState.Modified;
        appDbContext.SaveChanges();
        return img;
        }
    }
public interface IGalleryRepository
    {
    public Gallery Add( Gallery card );
    public Gallery Update( Gallery card );
    public Gallery Delete( int id );
    public IEnumerable<Gallery> GetPostByShopId( int id );
    public IEnumerable<Gallery> GetAll();

    }

