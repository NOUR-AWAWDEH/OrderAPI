using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Commands;
using OrdersAPI.Data;
using OrdersAPI.Dtos;
using OrdersAPI.Handlers;
using OrdersAPI.Models;
using OrdersAPI.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));

builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, OrderDto>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDto>, GetOrderByIdQueryHandler>();

var app = builder.Build();

app.MapPost("api/orders", async (ICommandHandler<CreateOrderCommand, OrderDto> handler, CreateOrderCommand command) =>
{
    var createOrder = await handler.HandleAsync(command);
    if (createOrder == null)
    {
        return Results.BadRequest();
    }
    return Results.Created($"/api/orders{createOrder.Id}", createOrder);
});


app.MapGet("api/orders/{id}", async (IQueryHandler<GetOrderByIdQuery, OrderDto> handler, int id) =>
{
    var order = await handler.HandleAsync(new GetOrderByIdQuery(id));
    if (order == null)
        return Results.NotFound();
    
    return Results.Ok(order);
});

app.Run();