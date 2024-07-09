using FluentValidation;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.IdentityModel.Tokens;

namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class UserDetails
    {
        public required string Uuid { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required bool IsActive { get; set; }
        public required RoleDetails Role { get; set; } = null!;
        public string? Avatar { get; set; }
    }

    public class UserShortDetails
    {
        public required string Uuid { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required bool IsActive { get; set; }
        public required RoleListView Role { get; set; } 
        public string? Avatar { get; set; }
    }

    public class UserListView
    {
        public required string Uuid { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required bool IsActive { get; set; }
        public string? Avatar { get; set; }
    }

    public class CreateUserRequest
    {
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
        public IFormFile? Avatar { get; set; } = null;
    }

    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator(IUserService userService, IRoleService roleService)
        {
            #region Check username
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("{PropertyName} must not be null or empty");
            When(x => !x.Name.IsNullOrEmpty(), () => RuleFor(x => x.Name).Must((username) => userService.CheckUsernameIfExists(username))
                                                                         .WithMessage("{PropertyName} has already existed"));
            #endregion

            #region Check email
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("{PropertyName} must not be null or empty")
                                 .EmailAddress().WithMessage("{PropertyName} is invalid");
            When(x => !x.Email.IsNullOrEmpty(), () => RuleFor(x => x.Email).Must((email) => !userService.CheckEmailIfExists(email))
                                                                           .WithMessage("{PropertyName} has been used"));
            #endregion

            #region Check fullname
            RuleFor(x => x.FullName).NotNull().NotEmpty().WithMessage("{PropertyName} must not be null or empty");
            #endregion

            #region Check password
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("{PropertyName} must not be null or empty");
            #endregion

            #region Check role
            RuleFor(x => x.Role).NotNull().NotEmpty().WithMessage("{PropertyName} must not be null or empty");
            When(x => !x.Role.IsNullOrEmpty(), () => RuleFor(x => x.Role).Must((roleName) => roleService.GetByName(roleName) != null)
                                                                         .WithMessage($"Role is not found"));
            #endregion
        }
    }
}
