using AutoMapper;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using Slugify;

namespace FoodRestaurantApp_BE.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<CreateUserRequest, SystemUser>().AfterMap((src, dest) => {
                dest.Uuid = Guid.NewGuid().ToString();
                dest.Password = BCrypt.Net.BCrypt.HashPassword(dest.Password);
                dest.IsActive = true;
            });
            CreateMap<CreateFoodRequest, Food>().ForMember(dest => dest.TypeId, 
                                                           src => src.MapFrom(prop => prop.FoodType))
                                                .AfterMap((src, dest) => {
                                                    SlugHelper slugHelper = new();
                                                    dest.Slug = slugHelper.GenerateSlug(dest.Name);
                                                });
            CreateMap<CreateFoodTypeRequest, FoodType>().AfterMap((src, dest) => {
                SlugHelper slugHelper = new();
                dest.Slug = slugHelper.GenerateSlug(dest.Name);
            });
        }
    }
}
