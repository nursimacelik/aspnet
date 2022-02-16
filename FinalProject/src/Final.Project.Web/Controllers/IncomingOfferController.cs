using Final.Project.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Final.Project.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomingOfferController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<IncomingOfferController> logger;

        public IncomingOfferController(ILogger<IncomingOfferController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var result = unitOfWork.Offer.Where(x => x.ProductOwnerId == userId);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await unitOfWork.Offer.GetById(id);
            var userId = GetUserId();
            if (offer == null || offer.ProductOwnerId != userId)
            {
                return NotFound();
            }
            return Ok(offer);
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> AnswerOffer(int id, bool accepted)
        {
            var offer = await unitOfWork.Offer.GetById(id);
            var userId = GetUserId();
            if (offer == null || offer.ProductOwnerId != userId)
            {
                return NotFound();
            }

            offer.Status = accepted ? "Accepted" : "Rejected";
            await unitOfWork.Offer.Update(offer);
            unitOfWork.Complete();
            return Ok(offer);
        }

        private int GetUserId()
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Int32.Parse(nameIdentifier);
        }
    }
}
