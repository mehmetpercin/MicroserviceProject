using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Order.Application.Commands;
using Services.Order.Application.Queries;
using SharedLibrary.Controllers;
using SharedLibrary.Services;

namespace Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _identityService;

        public OrdersController(IMediator mediator, ISharedIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _identityService.GetUserId });
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return CreateActionResultInstance(response);
        }
    }
}
