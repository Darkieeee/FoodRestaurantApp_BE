using FoodRestaurantApp_BE.Models.Requests;
using FoodRestaurantApp_BE.Models.Securities;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurantApp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Xác thực và cấp phát token cho người dùng là Customer
        /// </summary>
        /// <param name="request">Yêu cầu đăng nhập</param>
        /// <returns></returns>
        [HttpPost("customer/sign-in")]
        public IActionResult AuthenticateUserAsCustomer([FromBody] SignInRequest request)
        {
            AuthResult result = _authService.VerifyUser(request.Username, request.Password, true);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Xác thực và cấp phát token cho người dùng không phải là Customer
        /// </summary>
        /// <param name="request">Yêu cầu đăng nhập</param>
        /// <returns></returns>
        [HttpPost("admin/sign-in")]
        public IActionResult AuthenticateUserAsNotCustomer([FromBody] SignInRequest request)
        {
            AuthResult result = _authService.VerifyUser(request.Username, request.Password, false);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("customer/sign-up")]
        public IActionResult SignUpAsCustomer()
        {
            return Ok();
        }
    }
}
