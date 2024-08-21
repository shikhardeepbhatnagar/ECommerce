using AutoMapper;
using ShoppingCartAPI.Models.Dto;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
                config.CreateMap<CartHeader, EventBus.Messages.Events.Models.CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, EventBus.Messages.Events.Models.CartDetailsDto>().ReverseMap();
                config.CreateMap<ProductDto, EventBus.Messages.Events.Models.ProductDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
