using Final.Project.Domain.Entities;
using Final.Project.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Final.Project.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CategoryController> logger;

        public CategoryController(ILogger<CategoryController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await unitOfWork.Category.GetAll();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("/ById")]
        public async Task<IActionResult> GetById(int id = 0)
        {
            if(id == 0)
            {
                var result = await unitOfWork.Product.GetAll();
                return Ok(result);
            }
            var category = await unitOfWork.Category.GetById(id);
            if(category == null)
            {
                return NotFound();
            }

            var products = unitOfWork.Product.GetByCategory(id);

            if (products is null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category entity)
        {
            var response = await unitOfWork.Category.Add(entity);
            unitOfWork.Complete();

            return Ok();
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] Category entity)
        {
            var response = unitOfWork.Category.Update(entity);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
