using MicroservisProject.Web.Models.Order;
using MicroservisProject.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroservisProject.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;

        public OrderController(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.GetBasket();
            ViewBag.Basket = basket ?? new Models.Basket.BasketViewModel();

            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            // var orderStatus = await _orderService.CreateOrder(checkoutInfoInput); synchronous
            var orderStatus = await _orderService.SuspendOrder(checkoutInfoInput);
            if (!orderStatus.IsSuccessful)
            {
                var basket = await _basketService.GetBasket();
                ViewBag.Basket = basket ?? new Models.Basket.BasketViewModel();

                ViewBag.Error = orderStatus.ErrorMessage;
                return View();
            }

            // return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = orderStatus.OrderId }); // synchronous
            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = new Random().Next(1, 500) });
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _orderService.GetOrders());
        }
    }
}
