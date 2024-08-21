using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using OrderAPI.Models.Dto;
using OrderAPI.Repository;

namespace OrderAPI.Integration;
public class CartCheckoutEventHandler : IConsumer<CartCheckoutEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private ResponseDto _responseDto;
    public CartCheckoutEventHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _responseDto = new();
    }

    public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
    {
        if (context.Message.CartHeaderDto != null)
        {
            await _orderRepository.CreateOrder(_mapper.Map<CartHeaderDto>(context.Message.CartHeaderDto));
        }
    }
}
