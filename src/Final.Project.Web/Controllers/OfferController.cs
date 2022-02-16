using Final.Project.Domain.Entities;
using Final.Project.Domain.Interfaces;
using Final.Project.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Final.Project.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<OfferController> logger;

        public OfferController(ILogger<OfferController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var result = unitOfWork.Offer.Where(x => x.UserId == userId);
            if(result == null)
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
            if (offer == null || offer.UserId != userId)
            {
                return NotFound();
            }
            return Ok(offer);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OfferModel offerRequest)
        {
            var product = await unitOfWork.Product.GetById(offerRequest.ProductId);
            if (product == null)
            {
                return NotFound();
            }
            if (product.IsSold)
            {
                return BadRequest("Product is already sold!");
            }
            if (!product.IsOfferable)
            {
                return BadRequest("Product is not offerable!");
            }
            

            var userId = GetUserId();
            var existingOffer = unitOfWork.Offer.Where(x => x.UserId == userId && x.ProductId == offerRequest.ProductId).FirstOrDefault();
            if(existingOffer != null)
            {
                return BadRequest("You already have an offer for this product!");
            }

            var price = product.Price;
            decimal offerAmount;
            if (offerRequest.UsePercentage)
            {
                offerAmount = price * offerRequest.Percentage / 100;
            }
            else
            {
                offerAmount = offerRequest.Amount;
            }

            var offer = new Offer { ProductId = offerRequest.ProductId, UserId = userId, PriceOffered = offerAmount, ProductOwnerId = product.UserId };
            await unitOfWork.Offer.Add(offer);
            unitOfWork.Complete();
            return Ok(offer);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var offer = await unitOfWork.Offer.GetById(id);
            var userId = GetUserId();
            if (offer == null || offer.UserId != userId)
            {
                return NotFound("You don't have any offers for this product!");
            }
            await unitOfWork.Offer.Delete(id);
            unitOfWork.Complete();
            return Ok();
        }

        private int GetUserId()
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Int32.Parse(nameIdentifier);
        }
    }
}
