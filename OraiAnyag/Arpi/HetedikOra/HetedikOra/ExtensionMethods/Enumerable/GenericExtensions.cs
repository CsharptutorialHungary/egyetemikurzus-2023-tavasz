namespace HetedikOra.ExtensionMethods.Enumerable;

public static class GenericExtensions
{
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
    {
        foreach (var item in enumerable)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<T2> Transform<T1, T2>(this IEnumerable<T1> enumerable, Func<T1, T2> transform)
    {
        foreach (var item in enumerable)
        {
            yield return transform(item);
        }
    }
}