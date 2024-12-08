using EmptyStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmptyStore.Contexts
{
    public class ShopContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Email = "tom@mail.com", Password = "1111" }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Iphone 8", Price = 1000 },
                new Product { Id = 2, Name = "Iphone 10", Price = 2000 },
                new Product { Id = 3, Name = "Iphone 11", Price = 3000 },
                new Product { Id = 4, Name = "Iphone 12", Price = 4000 },
                new Product { Id = 5, Name = "Iphone 13", Price = 5000 },
                new Product { Id = 6, Name = "Iphone 14", Price = 6000 }
                );
        }
    }
}
