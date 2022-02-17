using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Order.Application.Dtos;
using Services.Order.Application.Mapping;
using Services.Order.Application.Queries;
using Services.Order.Infrastructure;
using SharedLibrary.Dtos;

namespace Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;
        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken = default)
        {
            var orders = await _context.Orders.AsNoTracking().Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync(cancellationToken);
            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }

            return Response<List<OrderDto>>.Success(ObjectMapper.Mapper.Map<List<OrderDto>>(orders), 200);
        }
    }
}
