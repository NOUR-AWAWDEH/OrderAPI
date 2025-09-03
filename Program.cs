using Microsoft.EntityFrameworkCore;
using OrdersAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));

var app = builder.Build();

app.Run();