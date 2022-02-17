using Services.Order.Domain.Core;

namespace Services.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public Order()
        {

        }

        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }

        public void AddOrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            var existsProduct = _orderItems.Any(x => x.ProductId == productId);
            if (!existsProduct)
            {
                _orderItems.Add(new OrderItem(productId, productName, pictureUrl, price));
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
