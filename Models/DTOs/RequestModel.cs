namespace FoodRestaurantApp_BE.Models.DTOs
{
    public abstract class RequestModel
    {
        public List<string> ErrorMessages { get; }

        protected RequestModel() {
            ErrorMessages = [];
        }

        public abstract void Validate();
        public bool HasAnyError()
        {
            return ErrorMessages.Any();
        }
    }
}
