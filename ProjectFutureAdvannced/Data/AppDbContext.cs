using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Extensions;
using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;

namespace ProjectFutureAdvannced.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
        {
        public DbSet<Admin> Admin { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> products  { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Post> posts { get; set; }  
        public DbSet<Gallery> galleries { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        public AppDbContext( DbContextOptions<AppDbContext> options ) : base(options)
            {
            }
        protected override void OnModelCreating( ModelBuilder modelBuilder )
            {
            base.OnModelCreating(modelBuilder);
            modelBuilder.CreateTable();
            modelBuilder.SetSeedRoles();
            modelBuilder.SetSeedCategory();
            //modelBuilder.setUniqueNameCategory();
            modelBuilder.setRelationShipProductShop();
            modelBuilder.CreateCardTable();
            modelBuilder.CreateWishListTable();

            modelBuilder.EditPost();
            //modelBuilder.CreateWishListTable();
            //modelBuilder.setRShipShop_Category();
            //modelBuilder.setRShipProduct_Shoper();
            // modelBuilder.relationShipProductUser();
            }
        }
    }
