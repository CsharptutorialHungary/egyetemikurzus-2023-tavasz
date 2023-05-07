using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D9MXN2.UI;

public class MainScreen : BaseScreen
{
 
    const string LOGOUT_COMMAND = "logout";

    Dictionary<string, Delegate> commands = new() {
        {"notes", () => System.Console.WriteLine()},
        {"add", () => System.Console.WriteLine()},
        {"delete", () => System.Console.WriteLine()},
        {"dump", () => System.Console.WriteLine()},
        {"load", () => System.Console.WriteLine()},
        {LOGOUT_COMMAND, () => Console.WriteLine("Goodbye")},
        {"exit", () => Environment.Exit(0)}
    };
    string _username;

    public MainScreen(string username) {
        _username = username;
    }

    public override void Render()
    {
        while(true) {
            Console.WriteLine($"You are logged in as *{_username}* user");
            string user_command = GetRequestedCommandFrom(commands.Keys);

            if(!commands.ContainsKey(user_command)) {
                Console.WriteLine($"[Error]: There is no command called *{user_command}*\n");
                continue;
            }
            if(user_command == LOGOUT_COMMAND) break;

            commands[user_command].DynamicInvoke();
        }
    }
}