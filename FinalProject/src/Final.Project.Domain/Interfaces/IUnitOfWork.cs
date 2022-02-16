using Final.Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> User { get; }
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IGenericRepository<Color> Color { get; }
        IGenericRepository<Brand> Brand { get; }
        IGenericRepository<Offer> Offer { get; }
        IGenericRepository<UsingStatus> UsingStatus { get; }

        int Complete();
    }
}
