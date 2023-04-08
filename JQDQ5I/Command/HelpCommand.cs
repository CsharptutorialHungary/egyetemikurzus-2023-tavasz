namespace GoogleBooks.Command;
public class HelpCommand : ICommand
{
    public string Name => "help";

    public void Execute(string[] args)
    {
        Console.WriteLine("commands:");
        Console.WriteLine("exit      exit from the application");
        Console.WriteLine("search    search for book");
        Console.WriteLine("          parameters: title  (-t/--title)");
        Console.WriteLine("                      author (-a/--author)");
        Console.WriteLine("                      isbn   (-i/--isbn)");
    }
}