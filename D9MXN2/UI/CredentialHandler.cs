using D9MXN2.Models;

namespace D9MXN2.UI;


public abstract class CredentialHandler : BaseScreen
{
    protected void RenderMain(string username)
    {
        HomeScreen main = new(username);
        main.Render($"You are logged in as *{username}* user.");
    }

    protected void CredentialHandling(Func<User, string> user_action)
    {
        const int MAX_TRIES = 3;
        int try_counter = 0;
        User? user = null;

        for (;try_counter < MAX_TRIES; try_counter++)
        {
            user = GetCredentails();
            Console.Clear();

            if (user.Username.Length >= 100 || user.Password.Length >= 100)
            {
                Console.WriteLine("[Error]: username and password must be smaller than 100 characters!");
            }

            string action_result = user_action(user);
            if (action_result.Length > 0)
            {
                Console.WriteLine($"[Error]: {action_result}");
                continue;
            }

            break;
        }

        if (try_counter >= MAX_TRIES)
        {
            Console.WriteLine("[Error]: Could not provide credentials.");
            return;
        }

        if(user != null) {
            RenderMain(user.Username);
        }
    }

    public User GetCredentails()
    {
        string username = GetInputWithMsg("Username");
        string password = GetInputWithMsg("Password"); //! FIXME: passwordRead from console 

        return new User(username, password);
    }
}