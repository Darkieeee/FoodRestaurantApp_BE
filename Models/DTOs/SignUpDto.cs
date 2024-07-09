using FluentValidation;
using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class SignUpDto
    {
        public required string Username { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public IFormFile? Avatar { get; set; }
    }

    public class SignUpValidator : AbstractValidator<SignUpDto>
    {
        public SignUpValidator(IUserService userService)
        {
            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("{PropertyName} không được bỏ trống")
                                    .Must((username) => !userService.CheckUsernameIfExists(username)).WithMessage("Username has already existed");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("{PropertyName} không được bỏ trống")
                                 .EmailAddress().WithMessage("{PropertyName} không hợp lệ")
                                 .Must((email) => !userService.CheckEmailIfExists(email)).WithMessage("Email has been used");
            RuleFor(x => x.FullName).NotNull().WithName("{PropertyName} không được bỏ trống");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithName("{PropertyName} không được bỏ trống");
        }
    }
}
