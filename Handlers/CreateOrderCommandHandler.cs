
using OrdersAPI.Commands;
using OrdersAPI.Data;
using OrdersAPI.Dtos;
using OrdersAPI.Models;

namespace OrdersAPI.Handlers;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, OrderDto>
{
    private readonly AppDbContext _context;

    public CreateOrderCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDto> HandleAsync(CreateOrderCommand command)
    {
        var order = new Order
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Status = command.Status,
            CreatedAt = DateTime.UtcNow,
            TotalCost = command.TotalCost
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

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