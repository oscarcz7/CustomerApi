﻿using CustomerApi.Models;
using CustomerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:8100").AllowAnyHeader()
                                                  .AllowAnyMethod(); ;
        });
});

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("CustomerOrderDatabase"));

// Add services to the container.
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

