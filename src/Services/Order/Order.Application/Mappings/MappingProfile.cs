using AutoMapper;
using Order.Application.Features.Orders.Commands.CheckoutOrder;
using Order.Application.Features.Orders.Commands.UpdateOrder;
using Order.Application.Features.Orders.Queries.GetOrdersList;
using Order.Domain.Entities;

namespace Order.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PurchaseOrder, OrdersVm>().ReverseMap();
            CreateMap<PurchaseOrder, CheckoutOrderCommand>().ReverseMap();
            CreateMap<PurchaseOrder, UpdateOrderCommand>().ReverseMap();
        }
    }
}
