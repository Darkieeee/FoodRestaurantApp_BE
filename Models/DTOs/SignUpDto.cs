using FluentValidation;

namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class SignUpDto
    {
        public required string Username { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class SignUpValidator : AbstractValidator<SignUpDto>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("{PropertyName} không được bỏ trống");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("{PropertyName} không được bỏ trống")
                                 .EmailAddress().WithMessage("{PropertyName} không hợp lệ");
            RuleFor(x => x.FullName).NotNull().WithName("{PropertyName} không được bỏ trống");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithName("{PropertyName} không được bỏ trống");
        }
    }
}
