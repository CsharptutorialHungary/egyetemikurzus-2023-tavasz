namespace GoogleBooks.Command;
public class ExitCommand : ICommand
{
    public string Name => "exit";

    public void Execute(string arg)
    {
        Environment.Exit(0);
    }
}