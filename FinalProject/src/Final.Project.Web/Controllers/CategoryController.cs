using Final.Project.Domain.Entities;
using Final.Project.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet("/Category/ById")]
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
        public async Task<IActionResult> Create([FromQuery] string name)
        {
            if (Exist(name))
            {
                return BadRequest($"{name} already exists.");
            }
            var item = new Category { Name = name };
            await unitOfWork.Category.Add(item);
            unitOfWork.Complete();
            return Ok(item);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            var item = await unitOfWork.Category.GetById(category.Id);
            if (item == null)
            {
                return NotFound();
            }
            if (Exist(category.Name))
            {
                return BadRequest($"{category.Name} already exists.");
            }

            item.Name = category.Name;
            await unitOfWork.Category.Update(item);
            unitOfWork.Complete();
            return Ok(item);
        }

        private bool Exist(string name)
        {
            var item = unitOfWork.Category.Where(x => x.Name.Equals(name)).FirstOrDefault();
            return item != null;
        }
    }
}
