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
    public class ColorController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ColorController> logger;

        public ColorController(ILogger<ColorController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await unitOfWork.Color.GetAll();
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
            var item = new Color { Name = name };
            await unitOfWork.Color.Add(item);
            unitOfWork.Complete();
            return Ok(item);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Color color)
        {
            var item = await unitOfWork.Color.GetById(color.Id);
            if (item == null)
            {
                return NotFound();
            }
            if (Exist(color.Name))
            {
                return BadRequest($"{color.Name} already exists.");
            }

            item.Name = color.Name;
            await unitOfWork.Color.Update(item);
            unitOfWork.Complete();
            return Ok(item);
        }

        private bool Exist(string name)
        {
            var item = unitOfWork.Color.Where(x => x.Name.Equals(name)).FirstOrDefault();
            return item != null;
        }

    }
}
