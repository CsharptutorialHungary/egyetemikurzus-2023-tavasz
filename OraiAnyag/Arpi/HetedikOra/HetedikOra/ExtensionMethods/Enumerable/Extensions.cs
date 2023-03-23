namespace HetedikOra.ExtensionMethods.Enumerable
{
    public static class Extensions
    {
        public static IEnumerable<int> GetEvenNumbers(this IEnumerable<int> numbers)
        {
            foreach (var number in numbers)
            {
                if (number % 2 == 0)
                {
                    yield return number;
                }
            }
        }

        public static IEnumerable<char> ToUpperChars(this IEnumerable<char> chars)
        {
            foreach (var c in chars)
            {
                yield return char.ToUpper(c);
            }
        }
    }
}
