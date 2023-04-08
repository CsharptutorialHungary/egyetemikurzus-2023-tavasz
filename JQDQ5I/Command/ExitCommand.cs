namespace GoogleBooks.Command;
public class ExitCommand : ICommand
{
    public string Name => "exit";

    public Task ExecuteAsync(string[] args)
    {
        Environment.Exit(0);

        return Task.CompletedTask;

    }
}