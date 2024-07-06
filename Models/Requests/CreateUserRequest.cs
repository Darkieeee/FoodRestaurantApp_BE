﻿using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Models.Requests
{
    public class CreateUserRequest
    {
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required int RoleId { get; set; }
    }
}