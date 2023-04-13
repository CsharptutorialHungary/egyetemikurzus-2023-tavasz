namespace reflection_linq
{
    public class ExitCommand : ICommand
    {
        public string Name => "exit";

        public void Execute()
        {
            Environment.Exit(0);
        }
    }
}

