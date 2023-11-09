using AutoMapper;
using Basket.API.Entities;
using EventBus.Messages.Events;

namespace Basket.API.Mapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, DeliveryAddress>().ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName));
            CreateMap<BasketCheckout, Payment>().ForMember(dest => dest.CardName, opt => opt.MapFrom(src => src.CardName));
            CreateMap<BasketCheckout, BasketCheckoutEvent>()
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => src)); ;




            //CreateMap<BasketCheckout, BasketCheckoutEvent>().ForPath(dest => dest.DeliveryAddress.FirstName, opt => opt.MapFrom(src => src.FirstName));

        }
    }
}
