using Core.Controllers;
using Core.NotifierErrors;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inventory.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductController : MainController<ProductController>
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService, INotifier notifier, ILogger<ProductController> logger) : base(notifier, logger)
        {
            _productService = productService;
        }

        [HttpGet("{productId:Guid}", Name = "GetProductPrice")]
        [ProducesResponseType(typeof(IEnumerable<Coupon>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetProductPrice(Guid productId)
        {
            try
            {
                var productPrice = await _productService.GetProductById(productId);
                return Ok(productPrice);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
