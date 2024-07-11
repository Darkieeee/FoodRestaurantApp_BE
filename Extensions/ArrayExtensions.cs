namespace FoodRestaurantApp_BE.Extensions
{
    public static class ArrayExtensions
    {
        public static bool Contains<T>(this T[] l1, T[] l2) where T: class
        {
            return l1.Intersect(l2).Any();
        }

        public static bool Contains<T>(this T[] l1, IEnumerable<T> l2) where T : class
        {
            return l1.Intersect(l2).Any();
        }
    }
}
