using Microsoft.EntityFrameworkCore;
using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FoodRestaurantApp_BE.Services.Abstracts;
using FoodRestaurantApp_BE.Repositories;
using Microsoft.OpenApi.Models;
using FoodRestaurantApp_BE.Middlewares;
using FoodRestaurantApp_BE.Helpers;
using FoodRestaurantApp_BE.Filters;
using FluentValidation;
using FoodRestaurantApp_BE.Models.DTOs;
using Microsoft.AspNetCore.HttpLogging;
using FoodRestaurantApp_BE.Extensions;
using FoodRestaurantApp_BE.Configurations;

var builder = WebApplication.CreateBuilder(args);

#region Add database to the container
builder.Services.AddDbContext<FoodRestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodRestaurantContext"))
);
#endregion

#region Add Jwt Security to the container 
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
#endregion

#region Add repositories to the container
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IFoodTypeRepository, FoodTypeRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
#endregion

#region Add loggers to the container
builder.Logging.AddFoodRestaurantFileLogger(options => {
    builder.Configuration.GetSection("Logging")
                         .GetSection("FoodRestaurantFile")
                         .GetSection("Options")
                         .Bind(options);
});
#endregion

#region Add services to the container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IOrderSystemService, OrderSystemService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IFoodTypeService, FoodTypeService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFileService, ImageFileService>();
builder.Services.AddScoped<IValidator<SignUpDto>, SignUpValidator>();
builder.Services.AddScoped<IValidator<CreateUserRequest>, CreateUserValidator>();
builder.Services.AddScoped<IValidator<CreateFoodTypeRequest>, CreateFoodTypeValidator>();
builder.Services.AddTransient<ITokenBlacklistService, JwtTokenBlacklistService>();
builder.Services.AddScoped<LoggingFilter>();
#endregion

#region Add automapper to the container
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
#endregion

#region Add swagger
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo() {
        Title = builder.Environment.ApplicationName,
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });; 
    c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});
#endregion

#region Add Redis to the container
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "FoodRestaurantRedis_";
});

/* The same way to connect the application to Redis
StackExchangeRedisCacheServiceCollectionExtensions.AddStackExchangeRedisCache(builder.Services, options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "FoodRestaurantRedis_";
});
*/
#endregion

#region Add exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
#endregion

#region Add http logging
builder.Services.AddHttpLogging(opt => { 
    opt.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders | HttpLoggingFields.RequestQuery 
                      | HttpLoggingFields.RequestBody | HttpLoggingFields.ResponsePropertiesAndHeaders; 
});
#endregion

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(opt => { });

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpLogging();

app.UseTokenValidation();

app.MapControllers();
app.MapSwagger().RequireAuthorization();

app.Run();