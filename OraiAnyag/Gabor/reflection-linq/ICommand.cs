namespace reflection_linq
{
    internal interface ICommand
    {
        string Name { get; }
        void Execute();
    }
}

