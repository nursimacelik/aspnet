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

namespace Final.Project.Infrastructure.CategoryRepo
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(FinalDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
