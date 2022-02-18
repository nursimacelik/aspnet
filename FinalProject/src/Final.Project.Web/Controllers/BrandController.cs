using Final.Project.Domain.Entities;
using Final.Project.Domain.Interfaces;
using Final.Project.Infrastructure.Uow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Project.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<BrandController> logger;

        public BrandController(ILogger<BrandController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await unitOfWork.Brand.GetAll();
            return Ok(result);
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string name)
        {
            if (Exist(name))
            {
                return BadRequest($"{name} already exists.");
            }
            var item = new Brand { Name = name };
            await unitOfWork.Brand.Add(item);
            unitOfWork.Complete();
            return Ok(item);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Brand brand)
        {
            var item = await unitOfWork.Brand.GetById(brand.Id);
            if(item == null)
            {
                return NotFound();
            }
            if (Exist(brand.Name))
            {
                return BadRequest($"{brand.Name} already exists.");
            }

            item.Name = brand.Name;
            await unitOfWork.Brand.Update(item);
            unitOfWork.Complete();
            return Ok(item);
        }

        private bool Exist(string name)
        {
            var item = unitOfWork.Brand.Where(x => x.Name.Equals(name)).FirstOrDefault();
            return item != null;
        }
    }
}
