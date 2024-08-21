using EventBus.Messages.Events;
using MediatR;
using ShoppingCartAPI.Models.Dto;

namespace ShoppingCartAPI.Commands
{
    public class CartCheckoutCommand : IRequest<ResponseDto>
    {
        public CartCheckoutEvent CartCheckoutEventModel { get; set; }
    }
}
