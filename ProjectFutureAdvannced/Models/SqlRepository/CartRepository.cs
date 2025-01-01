using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Data;
using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using System;
using System.Linq;

namespace ProjectFutureAdvannced.Models.SqlRepository;
    public class CartRepository: ICartRepository
    {
    private readonly AppDbContext _appDbContext;
    public CartRepository( AppDbContext _appDbContext )
    {
        this._appDbContext = _appDbContext;
    }
    public Card Add( Card card )
        {
        _appDbContext.Card.Add( card );
        _appDbContext.SaveChanges();
        return card;    
        }

    public IEnumerable<Product> GetAllProductByUserId( int userId )
        {
        return _appDbContext.Card
            .Where(card => card.UserId == userId)
            .Select(card => card.Product)
            .ToList();
    }
    public async Task<IEnumerable<Card>> DeleteAllCardByProductId( int ProductId )
        {
        var cardsToDelete = await _appDbContext.Card
            .Where(e => e.ProductId == ProductId)
            .ToArrayAsync(); // Execute the query and fetch the records to be deleted asynchronously

        if (cardsToDelete.Any())
            {
            _appDbContext.Card.RemoveRange(cardsToDelete);
            await _appDbContext.SaveChangesAsync();
            }

        return cardsToDelete;
        }
    public IEnumerable<Card> getAll()
        {
        return _appDbContext.Card;
        }
    public Card Delete(int Userid,int Productid )
        {
        var card = _appDbContext.Card.Find(Userid, Productid);
        if (card != null)
            {
             _appDbContext.Card.Remove(card);
            _appDbContext.SaveChanges();

            }
        return card;
        }
    }
public interface ICartRepository
    {
    public Card Add(Card card);
    public IEnumerable<Card> getAll();
    public Task<IEnumerable<Card>> DeleteAllCardByProductId( int ProductId );
    public IEnumerable<Product> GetAllProductByUserId( int userId );
    public Card Delete( int Userid,int Productid );
    }

