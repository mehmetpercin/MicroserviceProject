using MediatR;
using Services.Order.Application.Dtos;
using SharedLibrary.Dtos;

namespace Services.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
