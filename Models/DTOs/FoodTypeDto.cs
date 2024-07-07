using FluentValidation;
using System.ComponentModel;

namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class FoodTypeModelResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
    }

    public class CreateFoodTypeRequest
    {
        [DisplayName("Tên loại")]
        public required string Name { get; set; }
    }

    public class CreateFoodTypeValidator : AbstractValidator<CreateFoodTypeRequest>
    {
        public CreateFoodTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{ProperyName} không được bỏ trống");
        }
    }
}
