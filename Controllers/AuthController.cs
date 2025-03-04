﻿using FluentValidation;
using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Helpers;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurantApp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Xác thực và cấp phát token cho khách hàng
        /// </summary>
        /// <param name="request">Yêu cầu đăng nhập</param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] SignInRequest request)
        {    
            AuthResponse result = await _authService.VerifyUserAsync(request.Username, request.Password, false);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Xác thực và cấp phát token cho quản trị
        /// </summary>
        /// <param name="request">Yêu cầu đăng nhập</param>
        /// <returns></returns>
        [HttpPost("admin/login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsAdmin([FromBody] SignInRequest request)
        {
            AuthResponse result = await _authService.VerifyUserAsync(request.Username, request.Password, true);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Đăng xuất người dùng
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [ValidToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            LogoutDto result = await _authService.LogoutAsync(HttpContext.GetBearerToken()!);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp(IValidator<SignUpDto> validator,[FromForm] SignUpDto request)
        {
            var validated = await validator.ValidateAsync(request);

            if(!validated.IsValid)
            {
                StatusResponse errorResponse = new()
                {
                   Success = false,
                   Messages = validated.Errors.Select(x => x.ErrorMessage).ToList()
                };
                return BadRequest(errorResponse);
            }

            RegisterDto result = await _authService.SignUpAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public IActionResult RefreshToken()
        {
            throw new NotImplementedException("Not implemented yet");
        }
    }
}
