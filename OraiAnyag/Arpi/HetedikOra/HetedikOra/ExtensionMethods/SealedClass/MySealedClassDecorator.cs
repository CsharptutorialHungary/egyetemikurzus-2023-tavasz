namespace HetedikOra.ExtensionMethods.SealedClass;

public class MySealedClassDecorator
{
    private readonly MySealedClass _sealedClass;

    public MySealedClassDecorator(MySealedClass sealedClass)
    {
        _sealedClass = sealedClass;
    }

    public string Name
    {
        get => _sealedClass.Name;
        set => _sealedClass.Name = value;
    }

    public string GetFancyName()
    {
        return $"Fancy {_sealedClass.Name}";
    }
}