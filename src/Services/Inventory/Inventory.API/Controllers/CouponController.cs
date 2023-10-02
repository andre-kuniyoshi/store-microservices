using Core.Controllers;
using Core.NotifierErrors;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inventory.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CouponController : MainController<CouponController>
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService, INotifier notifier, ILogger<CouponController> logger) : base(notifier, logger)
        {
            _couponService = couponService;
        }

        [HttpGet("{code}", Name = "GetCouponBycode")]
        [ProducesResponseType(typeof(IEnumerable<Coupon>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCouponBycode(string code)
        {
            try
            {
                var coupon = await _couponService.GetCouponByCode(code);
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
        public async Task<IActionResult> CreateCoupon([FromBody] Coupon newCoupon)
        {
            try
            {
                var coupon = await _couponService.AddCoupon(newCoupon);
                return CreatedAtRoute("GetCoupon", new { productName = newCoupon.Id }, newCoupon);
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
        public async Task<IActionResult> UpdateCoupon([FromBody] Coupon newCoupon)
        {
            try
            {
                var coupon = await _couponService.AddCoupon(newCoupon);
                return Ok(newCoupon);
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{couponId}", Name = "DeleteCoupon")]
        [ProducesResponseType(typeof(IEnumerable<Coupon>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteCoupon(Guid couponId)
        {
            try
            {
                var products = await _couponService.RemoveCoupon(couponId);
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
