namespace GoogleBooks.Command;
public class HelpCommand : ICommand
{
    public string Name => "help";

    public Task ExecuteAsync(string[] args)
    {
        Console.WriteLine("commands:");
        Console.WriteLine("exit      exit from the application");
        Console.WriteLine("search    search for book (at least one parameter should be given)");
        Console.WriteLine("          parameters: title  (-t/--title)");
        Console.WriteLine("                      author (-a/--author)");
        Console.WriteLine("                      isbn   (-i/--isbn)");
        Console.WriteLine("usage: search -t vuk -a fekete");
        return Task.CompletedTask;
    }

}