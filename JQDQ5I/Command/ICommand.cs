namespace GoogleBooks.Command;
public interface ICommand
{
    string Name { get; }
    Task ExecuteAsync(string[] args);
}