using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Services.Payment.Models;
using SharedLibrary.Controllers;
using SharedLibrary.Messages;

namespace Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public PaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentInfoDto paymentInfoDto)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));
            var createOrderMessageCommand = new CreateOrderMessageCommand
            {
                BuyerId = paymentInfoDto.Order.BuyerId,
                District = paymentInfoDto.Order.Address.District,
                Line = paymentInfoDto.Order.Address.Line,
                Province = paymentInfoDto.Order.Address.Province,
                Street = paymentInfoDto.Order.Address.Street,
                ZipCode = paymentInfoDto.Order.Address.ZipCode,
                OrderItems = paymentInfoDto.Order.OrderItems.Select(x => new OrderItem
                {
                    PictureUrl = string.Empty,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                }).ToList()
            };

            await sendEndpoint.Send(createOrderMessageCommand);

            return CreateActionResultInstance(SharedLibrary.Dtos.Response<object>.Success(200));
        }
    }
}
