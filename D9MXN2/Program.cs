using D9MXN2;

class Program
{
    static Dictionary<string, Delegate> commands = new() {
        {"login", () => System.Console.WriteLine()},
        {"register", () => System.Console.WriteLine()},
        {"help", () => Help()},
        {"exit", () => Environment.Exit(0)}
    };

    static void Help() {
        Console.WriteLine("This is an awesome note handling application with user handling.");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("login: You can login here if you are registered.");
        Console.WriteLine("register: You can register here.");
        Console.WriteLine("exit: closes the application.");
        Console.WriteLine();
        Console.WriteLine("Press anything to proceed.");
        Console.ReadKey();
        Console.Clear();
    }

    static void MainScreen()
    {
        int try_counter = 0;
        while (try_counter++ < 3)
        {
            Console.WriteLine("What would you like to do?");

            foreach (string command_name in commands.Keys)
            {
                Console.WriteLine($"\t-{command_name}");
            }

            Console.Write(": ");
            string user_command = Console.ReadLine() ?? "";
            Console.Clear();

            if (!commands.ContainsKey(user_command))
            {
                Console.WriteLine($"There is no command called *{user_command}*");
                continue;
            }

            commands[user_command].DynamicInvoke();
        }
        Console.WriteLine("[Error]: could not provide valid input.");
        Environment.Exit(-1);
    }

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome to Awesome Note!");
            MainScreen();
        }
    }
}
