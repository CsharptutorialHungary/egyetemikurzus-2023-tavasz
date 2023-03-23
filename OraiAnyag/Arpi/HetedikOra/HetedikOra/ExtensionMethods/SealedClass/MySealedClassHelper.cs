namespace HetedikOra.ExtensionMethods.SealedClass;

public static class MySealedClassHelper
{
    public static string GetFancyName(this MySealedClass sealedClass)
    {
        return $"Fancy {sealedClass.Name}";
    }
}