using Microsoft.EntityFrameworkCore;
using OrdersAPI.Data;
using OrdersAPI.Dtos;
using OrdersAPI.Queries;


namespace OrdersAPI.Handlers;

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly AppDbContext _context;
    public GetOrderByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDto> HandleAsync(GetOrderByIdQuery query)
    {
        var order = await _context.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == query.OrderId)
                ?? throw new KeyNotFoundException($"Order with ID {query.OrderId} not found.");

        return new OrderDto(
            Id: order.Id,
            FirstName: order.FirstName,
            LastName: order.LastName,
            Status: order.Status,
            CreatedAt: order.CreatedAt,
            TotalCost: order.TotalCost
        );
    }
}
