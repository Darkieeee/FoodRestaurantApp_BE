using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Helpers;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Models.Requests;
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
        /// Xác thực và cấp phát token cho người dùng
        /// </summary>
        /// <param name="request">Yêu cầu đăng nhập</param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] SignInRequest request)
        {
            AuthDto result = await _authService.VerifyUserAsync(request.Username, request.Password);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Đăng xuất người dùng
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [Authorize]
        [ValidateTokenExpires]
        public async Task<IActionResult> Logout()
        {
            LogoutDto result = await _authService.LogoutAsync(HttpContext.GetBearerToken()!);
            return Ok(result);
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp(SignUpDto signupDto)
        {
            bool success = await _authService.SignUpAsync(signupDto);
            return success ? Ok("Đăng ký thành công") : BadRequest("Đăng ký thất bại");
        }

        [HttpPost("refresh-token")]
        [Authorize]
        [ValidateTokenExpires]
        public IActionResult RefreshToken()
        {
            throw new NotImplementedException("Not implemented yet");
        }
    }
}
