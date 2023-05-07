using D9MXN2;
using D9MXN2.UI;

class Program
{
    static Dictionary<string, Delegate> commands = new() {
        {"login", () => ScreenRender(new LoginScreen())},
        {"register", () => ScreenRender(new RegisterScreen())},
        {"help", () => Help()},
        {"exit", () => Environment.Exit(0)}
    };

    static void ScreenRender(BaseScreen screen)
    {
        screen.Render();
    }

    static void Help()
    {
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

    static void Main(string[] args)
    {
        const int MAX_TRIES = 3;

        while (true)
        {
            Console.WriteLine("Welcome to Awesome Note!");
         
            string user_command = "";
            int try_counter = 0;
            for (; try_counter < MAX_TRIES; try_counter++)
            {
                user_command = BaseScreen.GetRequestedCommandFrom(commands.Keys);

                if (commands.ContainsKey(user_command)) break;

                Console.WriteLine($"There is no command called *{user_command}*");
            }
            
            if(try_counter >= MAX_TRIES) {
                Console.WriteLine("[Error]: could not provide valid input.");
                Environment.Exit(-1);
            }
            
            commands[user_command].DynamicInvoke();
        }
    }
}
