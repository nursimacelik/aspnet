using Final.Project.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Project.Infrastructure.Context
{
    public interface IFinalDbContext
    {
        DbSet<User> User { get; set; }
        DbSet<Product> Product { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<Color> Color { get; set; }
        DbSet<Brand> Brand { get; set; }
        DbSet<Offer> Offer { get; set; }
        DbSet<UsingStatus> UsingStatus { get; set; }

        int SaveChanges();
    }
}
