using JoExport.Data.EfCore;
using JoExport.Data.Repository;
using JoExport.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace JoExport.Data.Repository;
      public class CardRepository: ICardRepository
    {
    private readonly AppDbContext appDbContext;
    public CardRepository( AppDbContext appDbContext )
        {
        this.appDbContext = appDbContext;
        }

    public Card Add( Card entity )
        {
        appDbContext.Card.Add(entity);
        appDbContext.SaveChanges();
        return entity;
        }

    public Card Delete( object id )
        {
        var user = appDbContext.Card.Find(id);
        if (user != null)
            {
            appDbContext.Card.Remove(user);
            appDbContext.SaveChanges();
            }
        return user;
        }

    public Card Find( Expression<Func<Card, bool>> predicate )
        {
        return appDbContext.Card.FirstOrDefault(predicate);
        }

    public IEnumerable<Card> Finds( Expression<Func<Card, bool>> predicate )
        {
        return appDbContext.Card.Where(predicate);
        }

    public IEnumerable<Card> GetAll()
        {
        return appDbContext.Card;
        }
    public Card Update( Card entity )
        {
        appDbContext.Card.Update(entity);
        appDbContext.SaveChanges();
        return entity;
        }
    }
    public interface ICardRepository:IRepository<Card>
    {
    }
