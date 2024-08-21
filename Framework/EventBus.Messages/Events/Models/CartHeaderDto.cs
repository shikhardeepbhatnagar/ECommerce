namespace EventBus.Messages.Events.Models
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public string? UserId { get; set; }
        public double CartTotal { get; set; }
        public ICollection<CartDetailsDto>? CartDetailsList { get; set; }
    }
}
