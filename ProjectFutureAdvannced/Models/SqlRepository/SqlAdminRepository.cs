using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Data;
using ProjectFutureAdvannced.Models.IRepository;
using ProjectFutureAdvannced.Models.Model.AccountUser;

namespace ProjectFutureAdvannced.Models.SqlRepository
{
    public class SqlAdminRepository : IAdminRepository
        {
        private readonly AppDbContext appDbContext;
        public SqlAdminRepository( AppDbContext appDbContext )
            {
            this.appDbContext = appDbContext;
            }
        public Admin Add( Admin Admin )
            {
            appDbContext.Admin.Add(Admin);
            appDbContext.SaveChanges();
            return Admin;
            }

        public Admin Delete( string id )
            {
            var Admin = appDbContext.Admin.FirstOrDefault(e=>e.UserId==id);
            if (Admin != null)
                {
                appDbContext.Admin.Remove(Admin);
                appDbContext.SaveChanges();
                }
            return Admin;
            }

        public Admin Get( int id )
            {
            var Admin = appDbContext.Admin.Find(id);
            return Admin;
            }

        public IEnumerable<Admin> GetAll()
            {
            return appDbContext.Admin;
            }

        public Admin GetByFk( string Fk )
            {
            return appDbContext.Admin.FirstOrDefault(e => e.UserId == Fk);
            }

        public Admin Update( Admin Admin )
            {
            var Admins = appDbContext.Admin.Attach(Admin);
            Admins.State = EntityState.Modified;
            appDbContext.SaveChanges();
            return Admin;
            }
    }
    }
