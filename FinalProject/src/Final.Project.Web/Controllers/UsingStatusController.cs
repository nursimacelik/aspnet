using Final.Project.Domain.Entities;
using Final.Project.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Project.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsingStatusController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<UsingStatusController> logger;

        public UsingStatusController(ILogger<UsingStatusController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await unitOfWork.UsingStatus.GetAll();
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

            var item = new UsingStatus { Name = name };
            await unitOfWork.UsingStatus.Add(item);
            unitOfWork.Complete();
            return Ok(item);
        }


        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UsingStatus status)
        {
            var item = await unitOfWork.UsingStatus.GetById(status.Id);
            if (item == null)
            {
                return NotFound();
            }
            if (Exist(status.Name))
            {
                return BadRequest($"{status.Name} already exists.");
            }

            item.Name = status.Name;
            await unitOfWork.UsingStatus.Update(item);
            unitOfWork.Complete();
            return Ok(item);
        }

        private bool Exist(string name)
        {
            var item = unitOfWork.UsingStatus.Where(x => x.Name.Equals(name)).FirstOrDefault();
            return item != null;
        }

    }
}
