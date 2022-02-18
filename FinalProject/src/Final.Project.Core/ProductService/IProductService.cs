using Final.Project.Core.Shared;
using Final.Project.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.Core.ProductServices
{
    public interface IProductService : ICRUDService<int, ProductDto, CreateProductInput, UpdateProductInput, User>
    {
        public Task<ApplicationResult<ProductDto>> UpdateImage(int productId, IFormFile file, User user);

    }
}
