﻿using Azure;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Models.Requests;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IAuthService
    {
        AuthDto VerifyUser(string username, string password);
        LogoutDto Logout(string tokenId);
        Task<AuthDto> VerifyUserAsync(string username, string password);
        Task<LogoutDto> LogoutAsync(string tokenId);
        RegisterDto Register(SignUpRequest newUser);
    }
}
