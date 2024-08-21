using AutoMapper;
using OrderAPI.Models;
using OrderAPI.Models.Dto;

namespace OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(dest=>dest.CartTotal, u=>u.MapFrom(src=>src.OrderTotal)).ReverseMap();

                config.CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));

                config.CreateMap<OrderDetailsDto, CartDetailsDto>();

                config.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
                config.CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();

                config.CreateMap<CartHeaderDto, EventBus.Messages.Events.Models.CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetailsDto, EventBus.Messages.Events.Models.CartDetailsDto>().ReverseMap();
                config.CreateMap<ProductDto, EventBus.Messages.Events.Models.ProductDto>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
