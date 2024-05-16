using Microsoft.EntityFrameworkCore;
using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FoodRestaurantApp_BE.Services.Abstracts;
using FoodRestaurantApp_BE.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add database to the container
builder.Services.AddDbContext<FoodRestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodRestaurantContext"))
);
// *****************************

// Add Jwt Security to the container
var secretKey = builder.Configuration["JwtBearer:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {
                    opt.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes)
                    };
                });
// *****************************

// Add repositories to the container
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
// *****************************

// Add services to the container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IOrderSystemService, OrderSystemService>();
builder.Services.AddScoped<IAuthService, AuthService>();
// *****************************

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
