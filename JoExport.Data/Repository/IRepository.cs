using System.Linq.Expressions;

namespace JoExport.Data.Repository
    {
    public interface IRepository<TEntity>
        {
        TEntity Add( TEntity entity );
        TEntity Delete( object id );
        TEntity Update( TEntity entity );
        IEnumerable<TEntity> Finds( Expression<Func<TEntity, bool>> predicate );
        TEntity Find( Expression<Func<TEntity, bool>> predicate );
        IEnumerable<TEntity> GetAll();
        }
    }
