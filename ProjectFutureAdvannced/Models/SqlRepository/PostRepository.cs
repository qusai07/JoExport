using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Data;
using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;

namespace ProjectFutureAdvannced.Models.SqlRepository;
public class PostRepository : IPostRepository
    {
    private readonly AppDbContext appDbContext;
    public PostRepository( AppDbContext appDbContext )
    {
        this.appDbContext=appDbContext;
    }
    public Post Add( Post card )
        {
        appDbContext.posts.Add(card);
        appDbContext.SaveChanges();
        return card;
        }

    public Post Delete( int id )
        {
        var Posts = appDbContext.posts.Find(id);
        if (Posts != null)
            {
            appDbContext.posts.Remove(Posts);
            appDbContext.SaveChanges();
            }
        return Posts;
        }

    public IEnumerable<Post> GetAll(  )
        {
        return appDbContext.posts;
        }

    public IEnumerable<Post> GetPostByShopId( int id )
        {
        return appDbContext.posts.Where(e => e.ShopId == id);
        }

    public Post Update( Post card )
        {
        var cardd = appDbContext.posts.Attach(card);
        cardd.State = EntityState.Modified;
        appDbContext.SaveChanges();
        return card;
        }
    public Post Get( int id )
        {
        return appDbContext.posts.Find(id);
        }
    }
public interface IPostRepository
    {
    public Post Add( Post card );
    public Post Update( Post card );
    public Post Get(int id );
    public Post Delete( int id );
    public  IEnumerable<Post> GetPostByShopId( int id );
    public IEnumerable<Post> GetAll( );
    }

