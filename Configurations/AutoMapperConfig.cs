using AutoMapper;
using Azure.Core;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.Requests;

namespace FoodRestaurantApp_BE.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<CreateUserRequest, SystemUser>().AfterMap((src, dest) => {
                dest.Password = BCrypt.Net.BCrypt.HashPassword(dest.Password);
                dest.IsActive = true;
            });    
        }
    }
}
