using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D9MXN2.DataAccess.Actions;

namespace D9MXN2.UI;

public class App : CommandScreen
{
    protected override Dictionary<string, Delegate> Commands { get; set; } = new() {
        {"login", () => ScreenRender(new LoginScreen())},
        {"register", () => ScreenRender(new RegisterScreen())},
        {"statistics", () => Statistics.PrintStatistics()},
        {"help", () => Help()},
        {"exit", () => Environment.Exit(0)}
    };

    static void Help()
    {
        Console.WriteLine("This is an awesome note handling application with user handling.");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("login: You can login here if you are registered.");
        Console.WriteLine("register: You can register here.");
        Console.WriteLine("statistics: Print statistics about the users.");
        Console.WriteLine("exit: closes the application.");
        Console.WriteLine();
        Console.WriteLine("Press anything to proceed.");
        Console.ReadKey();
        Console.Clear();
    }

    static void ScreenRender(BaseScreen screen)
    {
        screen.Render();
    }
}

public class HomeScreen : CommandScreen
{
    const string LOGOUT_COMMAND = "logout";
    protected override Dictionary<string, Delegate> Commands { get; set; } = new() {
        {"notes", () => System.Console.WriteLine()},
        {"add", () => System.Console.WriteLine()},
        {"delete", () => System.Console.WriteLine()},
        {"dump", () => System.Console.WriteLine()},
        {"load", () => System.Console.WriteLine()},
        {LOGOUT_COMMAND, () => Console.WriteLine("Goodbye")},
        {"exit", () => Environment.Exit(0)}
    };

    protected override bool SuccessfulCommandInvoke(string command)
    {
        if (command == LOGOUT_COMMAND) { return false; } // this will break the render loop

        return base.SuccessfulCommandInvoke(command);
    }
}