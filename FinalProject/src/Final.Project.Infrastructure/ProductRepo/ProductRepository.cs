using Final.Project.Domain.Entities;
using Final.Project.Domain.Interfaces;
using Final.Project.Infrastructure.Context;
using Final.Project.Infrastructure.Generic;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.Infrastructure.ProductRepo
{
    internal class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(FinalDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public IEnumerable<Product> GetByCategory(int categoryId)
        {
            return dbSet.Where(x => x.CategoryId == categoryId);
        }
    }
}
