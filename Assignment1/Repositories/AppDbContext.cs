using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Member> Member { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<OrderDetail> OrderDetail { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = Guid.NewGuid(),
                Name = "Laptop"
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Phone"
            });
            modelBuilder.Entity<Member>().HasData(new Member
            {
                Id = Guid.NewGuid(),
                City = "HoChiMinh",
                CompanyName = "FPT",
                Country = "VN",
                Email = "admin@gmail.com",
                Password = "sa123",
                RoleName = "ADMIN"

            });
        }
    }
}
