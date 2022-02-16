using Final.Project.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Final.Project.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PurchaseController> logger;

        public PurchaseController(ILogger<PurchaseController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpPut("{productId}")]
        public async Task<IActionResult> Buy(int productId)
        {
            var product = await unitOfWork.Product.GetById(productId);
            if(product == null)
            {
                return NotFound();
            }

            if(product.IsSold)
            {
                return BadRequest("Product is already sold.");
            }

            var userId = GetUserId();

            // Check if user has an offer for this product
            var offer = unitOfWork.Offer.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefault();
            
            // Decide to the price according to the status of offer, if exists
            decimal price;
            if (offer != null && offer.Status == "Accepted")
            {
                price = offer.PriceOffered;
            }
            else
            {
                price = product.Price;
            }

            // Update the product
            product.IsSold = true;
            await unitOfWork.Product.Update(product);
            unitOfWork.Complete();

            return Ok($"Purchase completed successfully. You purchased {product.ProductName} for ${price}.");

        }

        private int GetUserId()
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Int32.Parse(nameIdentifier);
        }
    }
}
