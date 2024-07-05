namespace FoodRestaurantApp_BE.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ValidTokenAttribute : Attribute { 
        public List<string>? ClaimType { get; set; }
    }
}
