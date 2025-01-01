using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoExport.Data.EfCore
    {
    public static class DatabaseInitializer
        {
        public static void CreateTable( this ModelBuilder modelBuilder )
            {
            modelBuilder.Entity<Admin>(entity => { entity.ToTable("Admin"); });
            modelBuilder.Entity<Shop>(entity => { entity.ToTable("Shop"); });
            modelBuilder.Entity<User>(entity => { entity.ToTable("User"); });
            }
        public static void CreateCardTable( this ModelBuilder modelBuilder )
            {
            modelBuilder.Entity<User>()
            .HasMany(e => e.Products)
            .WithMany(e => e.users)
            .UsingEntity<Card>(
                j => j
                .HasOne(e => e.Product)
                .WithMany(e => e.cards)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict),
                /*******************************/
                j => j
                .HasOne(e => e.User)
                .WithMany(e => e.cards)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict),
                j =>
                {
                    j.HasKey(e => new { e.UserId, e.ProductId });
                }
                );
            }
        public static void RelationShipGalleryShop( this ModelBuilder modelBuilder )
            {
            modelBuilder.Entity<Shop>()
                .HasMany(e => e.galleries)
                .WithOne(e => e.shop)
                .HasForeignKey(e => e.ShopId);
            }
        //public static void relationShipProductUser( this ModelBuilder modelBuilder )
        //    {
        //    modelBuilder.Entity<User>()
        //        .HasMany(e => e.Products)
        //        .WithOne(e => e.Buyer)
        //        .OnDelete(DeleteBehavior.Restrict);
        //    }
        public static void SetSeedRoles( this ModelBuilder modelBuilder )
            {
            var roles = new List<string>
                {
                "Admin",
                "User",
                "Shop"
                };
            foreach (var roleName in roles)
                {
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                    {
                    Id = Guid.NewGuid().ToString(),
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                    });
                }
            }
        public static void setUniqueNameCategory( this ModelBuilder modelBuilder )
            {
            modelBuilder.Entity<Category>()
                   .HasIndex(e => e.Name)
                   .IsUnique();
            }
        public static void setRelationShipProductShop( this ModelBuilder modelBuilder )
            {

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Products)
                .WithOne(e => e.shop)
                .HasPrincipalKey(e => e.Id)
                .HasForeignKey(E => E.ShopId);
            }
        //public static void setRShipShop_Category( this ModelBuilder modelBuilder )
        //    {
        //    modelBuilder.Entity<Shop>()
        //   .HasOne(shop => shop.Category)
        //   .WithOne(category => category.shop)
        //   .HasForeignKey<Shop>(shop => shop.CategoryName)
        //   .HasPrincipalKey<Category>(category => category.Name);
        //    }
        //public static void setRShipProduct_Shoper( this ModelBuilder modelBuilder )
        //    {

        //    modelBuilder.Entity<Product>()
        //            .HasOne(e => e.shop)
        //            .WithMany(e => e.Products)
        //            .HasPrincipalKey(e => e.Id)
        //            .HasForeignKey(e => e.ShoperId);
        //    }
        public static void SetSeedCategory( this ModelBuilder modelBuilder )
            {
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 1,
                Name = Categorys.Cars,
                CategoryImg = "cars.png",
                });
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 2,
                Name = Categorys.Electronic,
                CategoryImg = "Electronic.png",
                });
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 3,
                Name = Categorys.Home,
                CategoryImg = "home.png",
                });
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 4,
                Name = Categorys.Baby_Kids,
                CategoryImg = "Baby.png",
                });
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 5,
                Name = Categorys.Beauty,
                CategoryImg = "Beauty.png",
                });
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 6,
                Name = Categorys.Clothes,
                CategoryImg = "Clothes.png",
                });
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 7,
                Name = Categorys.Bags,
                CategoryImg = "Bags.png",
                });
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 8,
                Name = Categorys.Books,
                CategoryImg = "Books.png",
                });
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 9,
                Name = Categorys.Health_and_Personal_Care,
                CategoryImg = "Health.png",
                });
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 10,
                Name = Categorys.Jewelry,
                CategoryImg = "Jewelry.png",
                });
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 11,
                Name = Categorys.pets,
                CategoryImg = "pets.png",
                });
            modelBuilder.Entity<Category>().HasData(new Category
                {
                Id = 12,
                Name = Categorys.Sport_Fitness,
                CategoryImg = "Sport.png",
                });
            }
        }
    }
