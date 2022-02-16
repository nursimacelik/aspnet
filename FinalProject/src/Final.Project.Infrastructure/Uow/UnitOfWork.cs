using Final.Project.Domain.Entities;
using Final.Project.Domain.Interfaces;
using Final.Project.Infrastructure.CategoryRepo;
using Final.Project.Infrastructure.Context;
using Final.Project.Infrastructure.Generic;
using Final.Project.Infrastructure.ProductRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.Infrastructure.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private readonly FinalDbContext context;

        public IGenericRepository<User> User { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IGenericRepository<Color> Color { get; private set; }
        public IGenericRepository<Brand> Brand { get; private set; }
        public IGenericRepository<Offer> Offer { get; private set; }
        public IGenericRepository<UsingStatus> UsingStatus { get; private set; }

        public UnitOfWork(FinalDbContext context, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            this.context = context;
            this.logger = loggerFactory.CreateLogger("Unit of work");
            this.configuration = configuration;

            User = new GenericRepository<User>(context, logger);
            Product = new ProductRepository(context, logger);
            Category = new CategoryRepository(context, logger);
            Color = new GenericRepository<Color>(context, logger);
            Brand = new GenericRepository<Brand>(context, logger);
            Offer = new GenericRepository<Offer>(context, logger);
            UsingStatus = new GenericRepository<UsingStatus>(context, logger);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
