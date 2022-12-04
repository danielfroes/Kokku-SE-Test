using System.Collections.Generic;

namespace AutoBattle
{
    public static class ListExtensions
    {
        public static T GetRandomElement<T>(this IReadOnlyList<T> list)
        {
            if (list.IsNullOrEmpty())
            {
                return default;
            }

            int randomIndex = Utils.GetRandomNumber(0, list.Count);

            return list[randomIndex];
        }

        public static bool IsNullOrEmpty<T>(this IReadOnlyList<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
