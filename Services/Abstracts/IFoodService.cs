namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IFoodService
    {
        void GetAll();
        void GetById();
        void Insert();
        void Update();
        void Delete();
    }
}
