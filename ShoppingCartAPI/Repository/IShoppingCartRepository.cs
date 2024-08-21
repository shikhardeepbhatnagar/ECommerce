using ShoppingCartAPI.Models.Dto;

namespace ShoppingCartAPI.Repository
{
    public interface IShoppingCartRepository
    {
        Task<ResponseDto> GetShoppingCart(string userId, CancellationToken cancellationToken = default);
        Task<ResponseDto> AddShoppingCart(CartHeaderDto cartHeaderDto, CancellationToken cancellationToken = default);
        Task<ResponseDto> DeleteShoppingCart(int cartDetailsId, CancellationToken cancellationToken = default);
        Task<ResponseDto> DeleteEntireShoppingCart(string userId, CancellationToken cancellationToken = default);
    }
}
