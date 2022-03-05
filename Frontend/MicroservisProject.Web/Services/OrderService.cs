using MicroservisProject.Web.Models.Order;
using MicroservisProject.Web.Models.Payment;
using MicroservisProject.Web.Services.Interfaces;
using SharedLibrary.Dtos;
using SharedLibrary.Services;

namespace MicroservisProject.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }
        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput input)
        {
            var basket = await _basketService.GetBasket();
            var payment = new PaymentInfoInput
            {
                CardName = input.CardName,
                CardNumber = input.CardNumber,
                CVV = input.CVV,
                Expiration = input.Expiration,
                TotalPrice = basket.TotalPrice
            };
            var responsePayment = await _paymentService.ReceivePayment(payment);
            if (!responsePayment)
            {
                return new OrderCreatedViewModel { ErrorMessage = "Ödeme başarısız", IsSuccessful = false };
            }

            var response = await _httpClient.PostAsJsonAsync("orders", new OrderCreateInput
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AdressCreateInput
                {
                    District = input.District,
                    Line = input.Line,
                    Province = input.Province,
                    Street = input.Street,
                    ZipCode = input.ZipCode
                },
                OrderItems = basket.BasketItems.Select(x => new OrderItemViewModel { Price = x.CurrentPrice, ProductId = x.CourseId, ProductName = x.CourseName, PictureUrl = string.Empty }).ToList()
            });

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel { ErrorMessage = "Sipariş oluşturulamadı", IsSuccessful = false };
            }

            await _basketService.DeleteBasket();

            var orderResponse = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
            return orderResponse.Data;
        }

        public async Task<List<OrderViewModel>> GetOrders()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput input)
        {
            var basket = await _basketService.GetBasket();
            var orderCreateInput = new OrderCreateInput
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AdressCreateInput
                {
                    District = input.District,
                    Line = input.Line,
                    Province = input.Province,
                    Street = input.Street,
                    ZipCode = input.ZipCode
                },
                OrderItems = basket.BasketItems.Select(x => new OrderItemViewModel { Price = x.CurrentPrice, ProductId = x.CourseId, ProductName = x.CourseName, PictureUrl = string.Empty }).ToList()
            };
            var payment = new PaymentInfoInput
            {
                CardName = input.CardName,
                CardNumber = input.CardNumber,
                CVV = input.CVV,
                Expiration = input.Expiration,
                TotalPrice = basket.TotalPrice,
                Order = orderCreateInput
            };

            var responsePayment = await _paymentService.ReceivePayment(payment);
            if (!responsePayment)
            {
                return new OrderSuspendViewModel { ErrorMessage = "Ödeme başarısız", IsSuccessful = false };
            }
            await _basketService.DeleteBasket();

            return new OrderSuspendViewModel { IsSuccessful = true };
        }
    }
}
