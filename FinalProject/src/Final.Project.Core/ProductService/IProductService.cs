using Final.Project.Core.Shared;
using Final.Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final.Project.Core.ProductServices
{
    public interface IProductService : ICRUDService<int, ProductDto, CreateProductInput, UpdateProductInput, User>
    {
    }
}
