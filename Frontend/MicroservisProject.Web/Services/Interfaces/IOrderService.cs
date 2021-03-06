using MicroservisProject.Web.Models.Order;

namespace MicroservisProject.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput input);
        Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput input); // asynchronous
        Task<List<OrderViewModel>> GetOrders();
    }
}
