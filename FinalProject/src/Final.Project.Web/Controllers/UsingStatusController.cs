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
        public async Task<IActionResult> Create([FromBody] string name)
        {
            var status = new UsingStatus { Name = name };
            var statusExists = await Exist(status.Id);
            if (statusExists)
            {
                return BadRequest("Status Id already exists.");
            }

            var result = await unitOfWork.UsingStatus.Add(status);
            unitOfWork.Complete();
            return Ok(result);
        }


        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UsingStatus status)
        {
            var statusExists = await Exist(status.Id);
            if (!statusExists)
            {
                return NotFound();
            }
            
            // Check if any product has this status
            if (InUsage(status.Id))
            {
                return BadRequest("Status cannot be updated, some products are using it.");
            }

            var result = await unitOfWork.UsingStatus.Update(status);
            unitOfWork.Complete();
            return Ok(result);
        }


        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UsingStatus status)
        {
            var statusExists = await Exist(status.Id);
            if (!statusExists)
            {
                return NotFound();
            }

            // Check if any product has this status
            if (InUsage(status.Id))
            {
                return BadRequest("Status cannot be deleted, some products are using it.");
            }

            await unitOfWork.UsingStatus.Delete(status.Id);
            unitOfWork.Complete();
            return Ok();
        }

        private bool InUsage(int id)
        {
            var productList = unitOfWork.Product.Where(x => x.UsingStatusId == id).FirstOrDefault();
            return productList != null;
        }

        private async Task<bool> Exist(int id)
        {
            var item = await unitOfWork.UsingStatus.GetById(id);
            return item != null;
        }


    }
}
