using D9MXN2.Models;

namespace D9MXN2.UI;


public abstract class CredentialHandler: IScreen
{
    protected string GetCredentialWithMsg(string msg) {
        Console.Write(msg + ":");

        return Console.ReadLine() ?? "";
    }

    protected void RenderMain() {
        MainScreen main = new();
        main.Render();
    }

    protected void CredentialHandling(Func<User, string> user_action) {
        const int MAX_TRIES = 3;
        int try_counter = 0;

        while (try_counter++ < MAX_TRIES)
        {
            var user = GetCredentails();
            Console.Clear();

            string action_result = user_action(user);
            if (action_result.Length > 0)
            {
                Console.WriteLine($"[Error]: {action_result}");
                continue;
            }

            Console.WriteLine($"Success! Your username is {user.Username}");
            break;
        }

        if(try_counter >= MAX_TRIES) {
            Console.WriteLine("[Error]: Could not provide credentials.");
            return;
        }

        this.RenderMain();
    }

    public User GetCredentails() {
        string username = GetCredentialWithMsg("Username");
        string password = GetCredentialWithMsg("Password"); //! FIXME: passwordRead from console 

        return new User(username, password);
    }

    public abstract void Render();

}