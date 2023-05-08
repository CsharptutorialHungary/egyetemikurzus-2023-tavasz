namespace D9MXN2.UI;

public abstract class BaseScreen
{
    public static string GetInputWithMsg(string msg)
    {
        Console.Write(msg + ": ");
        return Console.ReadLine() ?? "";
    }

    public static string GetChosenCommandFrom(IEnumerable<string> command_keys)
    {
        Console.WriteLine("What would you like to do?");

        foreach (string command_name in command_keys)
        {
            Console.WriteLine($"\t-{command_name}");
        }

        string user_command = GetInputWithMsg(" ");
        Console.Clear();

        return user_command;
    }

    public abstract void Render(string? msg = default);
}