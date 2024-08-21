using ProductAPI.Models.Dto;

namespace ProductAPI.Service
{
    public interface IProductDetailService
    {
        Task<IEnumerable<ProductDetailDto>> GetProductDetail();

        Task<IEnumerable<ProductDetailDto>> SaveProductDetail();
    }
}
