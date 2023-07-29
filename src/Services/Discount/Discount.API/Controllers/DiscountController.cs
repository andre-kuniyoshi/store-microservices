using Core.Controllers;
using Core.NotifierErrors;
using Discount.Domain.Entities;
using Discount.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class DiscountController : MainController<DiscountController>
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService, INotifier notifier, ILogger<DiscountController> logger) : base(notifier, logger)
        {
            _discountService = discountService;
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(IEnumerable<Coupon>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetDiscount(string productName)
        {
            try
            {
                var coupon = await _discountService.GetDiscount(productName);
                return Ok(coupon);
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Coupon>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateDiscount([FromBody] Coupon newCoupon)
        {
            try
            {
                var coupon = await _discountService.AddDiscount(newCoupon);
                return CreatedAtRoute("GetDiscount", new { productName = newCoupon.ProductName }, newCoupon);
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Coupon>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon newCoupon)
        {
            try
            {
                var coupon = await _discountService.AddDiscount(newCoupon);
                return Ok(newCoupon);
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(IEnumerable<Coupon>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            try
            {
                var products = await _discountService.RemoveDiscount(productName);
                return Ok(products);
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
