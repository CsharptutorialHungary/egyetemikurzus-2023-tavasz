namespace GoogleBooks.Command;
public interface ICommand
{
    string Name { get; }
    void Execute(string arg);
}