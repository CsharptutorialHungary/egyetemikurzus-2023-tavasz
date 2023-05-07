namespace D9MXN2.UI;

public abstract class BaseScreen
{
    public abstract void Render();

    public static string GetInputWithMsg(string msg)
    {
        Console.Write(msg + ":");

        return Console.ReadLine() ?? "";
    }

    public static string GetRequestedCommandFrom(IEnumerable<string> command_keys) {
        Console.WriteLine("What would you like to do?");

        foreach (string command_name in command_keys)
        {
            Console.WriteLine($"\t-{command_name}");
        }

        Console.Write(": ");
        string user_command = Console.ReadLine() ?? "";
        Console.Clear();
    
        return user_command;
    }
}