namespace GoogleBooks.Command;
public class ClearCommand : ICommand
{
    public string Name => "clear";

    public Task ExecuteAsync(string[] args)
    {
        Console.Clear();
        return Task.CompletedTask;
    }

}