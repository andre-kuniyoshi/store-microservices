using AutoMapper;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Services;
using Inventory.Grpc.Protos;
using Grpc.Core;

namespace Inventory.Grpc.Services
{
    public class ProductGrpcService : ProductProtoService.ProductProtoServiceBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductGrpcService> _logger;

        public ProductGrpcService(IProductService productService, IMapper mapper, ILogger<ProductGrpcService> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<ProductModel> GetProductPrice(GetProductPriceRequest request, ServerCallContext context)
        {
            var product = await _productService.GetProductById(Guid.Parse(request.Id));

            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName = {request.Id} is not found."));
            }

            _logger.LogInformation($"Product price is retrieved for ProductId: {product.Id}");

            var productModel = _mapper.Map<ProductModel>(product);

            return productModel;
        }
    }
}
