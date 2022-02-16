using Final.Project.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Project.Infrastructure.Context
{
    public class FinalDbContext : DbContext, IFinalDbContext
    {
        public FinalDbContext(DbContextOptions<FinalDbContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Offer> Offer { get; set; }
        public DbSet<UsingStatus> UsingStatus { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
