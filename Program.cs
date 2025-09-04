using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Commands;
using OrdersAPI.Data;
using OrdersAPI.Dtos;
using OrdersAPI.Handlers;
using OrdersAPI.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));

// Register handlers and validators
builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, OrderDto>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDto>, GetOrderByIdQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetOrderSummariesQuery, List<OrderSummaryDto>>, GetOrderSummariesQueryHandler>();
builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
builder.Services.AddScoped<IValidator<GetOrderByIdQuery>, GetOrderByIdQueryValidator>();


var app = builder.Build();

app.MapPost("api/orders", async (ICommandHandler<CreateOrderCommand, OrderDto> handler, CreateOrderCommand command) =>
{
    try
    {
        var createOrder = await handler.HandleAsync(command);
        if (createOrder == null)
        {
            return Results.BadRequest();
        }

        return Results.Created($"/api/orders{createOrder.Id}", createOrder);
    }
    catch (ValidationException ex)
    {
        var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
        return Results.BadRequest(errors);
    }
    
});

app.MapGet("api/orders/{id}", async (IQueryHandler<GetOrderByIdQuery, OrderDto> handler, int id) =>
{
    try
    {

        var order = await handler.HandleAsync(new GetOrderByIdQuery(id));
        if (order == null)
            return Results.NotFound();

        return Results.Ok(order);
    }
    catch (ValidationException ex)
    {
        var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
        return Results.BadRequest(errors);
    }

});

app.MapGet("api/orders", async (IQueryHandler<GetOrderSummariesQuery, List<OrderSummaryDto>> handler) =>
{
    var summaries = await handler.HandleAsync(new GetOrderSummariesQuery());
    return Results.Ok(summaries);
});

app.Run();