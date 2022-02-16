using Final.Project.Domain.Interfaces;
using Final.Project.Infrastructure.Uow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

    }
}
