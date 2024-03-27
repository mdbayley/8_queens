
namespace _8_Queens
{
    internal static class Extensions
    {
        public static void Clear(this GridMap[] maps)
        {
            foreach (var map in maps)
            {
                map.Clear();
            }
        }
    }
}
