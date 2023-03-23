using HetedikOra.ExtensionMethods.Enumerable;
using HetedikOra.ExtensionMethods.SealedClass;
using WithEnumerator = HetedikOra.Enumerable.UsingEnumerator;
using WithYield = HetedikOra.Enumerable.UsingYield;

#region Collection with Enumerator

Console.WriteLine("== Enumerator Test ==");
{
    var collection = new WithEnumerator.IntegerCollection { 1, 2, 3, 4, 5 };

    foreach (var item in collection)
    {
        Console.WriteLine($"-- item: {item}");
    }
}
Console.WriteLine();

#endregion

#region Collection with Yield

Console.WriteLine("== Yield Test ==");
{
    var collection = new WithYield.IntegerCollection { 1, 2, 3, 4, 5 };

    foreach (var item in collection)
    {
        Console.WriteLine($"-- item: {item}");
    }
}
Console.WriteLine();

#endregion

#region Extending with Decorator

Console.WriteLine("== Decorator Test ==");
{
    var sealedClass = new MySealedClass { Name = "Darth Vader" };
    var decoratedClass = new MySealedClassDecorator(sealedClass);

    var fancyName = decoratedClass.GetFancyName();
    Console.WriteLine($"-- fancy name: {fancyName}");
}
Console.WriteLine();

#endregion

#region Extending with Extension Method

Console.WriteLine("== Extension Method Test ==");
{
    var sealedClass = new MySealedClass { Name = "Darth Vader" };

    var fancyName = sealedClass.GetFancyName();
    Console.WriteLine($"-- fancy name: {fancyName}");
}
Console.WriteLine();

#endregion

#region Filtering and Transforming

Console.WriteLine("== Filtering and Transforming Test ==");
{
    var numbers = new[] { 1, 2, 3, 4, 5 };
    var evenNumbers = numbers.GetEvenNumbers();

    Console.WriteLine($"-- even numbers: {string.Join(", ", evenNumbers)}");

    var characters = "Hello, World!";
    var upperCharacters = characters.ToUpperChars();

    Console.WriteLine($"-- upper characters: {string.Join("", upperCharacters)}");
}
Console.WriteLine();

#endregion

#region Filtering and Transforming with Delegates

Console.WriteLine("== Delegate Test ==");
{
    bool IsEvenNumber(int number)
    {
        return number % 2 == 0;
    }

    char ToUpperChar(char c)
    {
        return char.ToUpper(c);
    }

    var numbers = new[] { 1, 2, 3, 4, 5 };
    var evenNumbers = numbers.Filter(IsEvenNumber);

    Console.WriteLine($"-- even numbers: {string.Join(", ", evenNumbers)}");

    var characters = "Hello, World!";
    var upperCharacters = characters.Transform(ToUpperChar);

    Console.WriteLine($"-- upper characters: {string.Join("", upperCharacters)}");
}
Console.WriteLine();

#endregion