namespace MicroservisProject.Web.Models.Order
{
    public class OrderCreateInput
    {
        public string BuyerId { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public AdressCreateInput Address { get; set; }
    }
}
