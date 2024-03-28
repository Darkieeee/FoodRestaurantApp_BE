using Microsoft.EntityFrameworkCore;
using FoodRestaurantApp_BE.Contexts;
using FoodRestaurantApp_BE.Services;
using FoodRestaurantApp_BE.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// Add database to the container
builder.Services.AddDbContext<FoodRestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodRestaurantContext"))
);

// Add services to the container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderSystemService, OrderSystemService>();
builder.Services.AddScoped<IPayOsOneTimePaymentService, PayOsOneTimePaymentService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
