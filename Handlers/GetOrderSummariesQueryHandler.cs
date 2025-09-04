
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Data;
using OrdersAPI.Dtos;
using OrdersAPI.Queries;

namespace OrdersAPI.Handlers;

public class GetOrderSummariesQueryHandler(AppDbContext context) : IQueryHandler<GetOrderSummariesQuery, List<OrderSummaryDto>>
{
    private readonly AppDbContext _context = context;

    public async Task<List<OrderSummaryDto>> HandleAsync(GetOrderSummariesQuery query)
    {
        return await _context.Orders
            .Select(o => new OrderSummaryDto
            (
                o.Id,
                o.FirstName + " " + o.LastName,
                o.Status,
                o.TotalCost
            )).ToListAsync();
    }
}
