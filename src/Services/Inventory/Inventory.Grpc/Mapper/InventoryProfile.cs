using AutoMapper;
using Inventory.Domain.Entities;
using Inventory.Grpc.Protos;

namespace Inventory.Grpc.Mapper
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            //CreateMap<Product, ProductModel>();

            CreateMap<Product, ProductModel>()
                .ForMember(dst => dst.CreatedDate,
                    map => map.MapFrom(src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(src.CreatedDate)))
                .ForMember(dst => dst.LastModifiedDate,
                    map => map.MapFrom(src => src.LastModifiedDate != null ? Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset((DateTime)src.LastModifiedDate) : null));

        }
    }
}
