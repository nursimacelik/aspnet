using Final.Project.Core.ProductServices;
using Final.Project.Core.Shared;
using Final.Project.Domain.Entities;
using Final.Project.Domain.Interfaces;
using Final.Project.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Final.Project.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<BrandController> logger;
        private readonly IProductService productService;

        public ProductController(ILogger<BrandController> logger, IUnitOfWork unitOfWork, IProductService productService)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.productService = productService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ApplicationResult<List<ProductDto>>> GetAll()
        {
            var user = await GetCurrentUserAsync();
            return await productService.GetAll(user);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationResult<ProductDto>>> GetById(int id)
        {
            var user = await GetCurrentUserAsync();
            var result = await productService.Get(id, user);
            if (result.Success)
            {
                return result;
            }
            return NotFound(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ApplicationResult<ProductDto>>> Create([FromBody] CreateProductInput input)
        {
            var user = await GetCurrentUserAsync();
            var result = await productService.Create(input, user);
            if (result.Success)
            {
                return result;
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ApplicationResult<ProductDto>>> Update([FromBody] UpdateProductInput input)
        {
            if(ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var result = await productService.Update(input, user);
                if (result.Success)
                {
                    return result;
                }
                return BadRequest(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        private async Task<User> GetCurrentUserAsync()
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var id = Int32.Parse(nameIdentifier);
            return await unitOfWork.User.GetById(id);
        }


        [Authorize]
        [HttpPut("/UpdateImage/{productId}")]
        public async Task<ActionResult<ApplicationResult<ProductDto>>> UpdateImage(int productId, [FromForm] IFormFile file)
        {
            var user = await GetCurrentUserAsync();
            var result = await productService.UpdateImage(productId, file, user);
            if (result.Success)
            {
                return result;
            }
            return BadRequest(result);
        }

        private bool IsImage(Stream stream)
        {
            // png, jpg, jpeg
            return true;

        }
    }
}
