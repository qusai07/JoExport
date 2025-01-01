using JoExport.Domain.Model;
using JoExport.Domain.Model.AccountUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoExport.Data.EfCore
    {
    public class AppDbContext:IdentityDbContext<AppUser>
        {
        public AppDbContext( DbContextOptions<AppDbContext> options ) : base(options) { }
        protected override void OnModelCreating( ModelBuilder modelBuilder )
            {
            base.OnModelCreating(modelBuilder);
            /*****************************/
            modelBuilder.CreateTable();
            modelBuilder.SetSeedRoles();
            modelBuilder.SetSeedCategory();
            modelBuilder.setUniqueNameCategory();
            modelBuilder.setRelationShipProductShop();
            modelBuilder.CreateCardTable();
            }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Card> Card { get; set; }
        }
    }
