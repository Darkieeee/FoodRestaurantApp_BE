namespace FoodRestaurantApp_BE.Models.Dto
{    
    public class FoodVariantDto(int id, string name, string foodId) {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string FoodId { get; set; } = foodId;
    }
}
