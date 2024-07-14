using FluentValidation;
using System.ComponentModel;

namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class FoodTypeListView
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
    }

    public class StoreUpdateFoodTypeRequest
    {
        [DisplayName("Tên loại")]
        public required string Name { get; set; }
    }

    public class StoreUpdateFoodTypeValidator : AbstractValidator<StoreUpdateFoodTypeRequest>
    {
        public StoreUpdateFoodTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{ProperyName} không được bỏ trống");
        }
    }
}
