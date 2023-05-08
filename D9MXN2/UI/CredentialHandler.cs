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
        const int MAX_TRIES = 5;
        const int MIN_CHAR_COUNT = 4;
        const int MAX_CHAR_COUNT = 100; // from db models

        int try_counter = 0;
        User? user = null;

        for (; try_counter < MAX_TRIES; ++try_counter)
        {
            user = GetCredentails();
            Console.Clear();

            if (user.Username.Length >= MAX_CHAR_COUNT || user.Password.Length >= MAX_CHAR_COUNT)
            {
                Console.WriteLine($"[Error]: username and password must be smaller than {MAX_CHAR_COUNT} characters!");
                continue;
            }

            if (user.Username.Length < MIN_CHAR_COUNT || user.Password.Length < MIN_CHAR_COUNT)
            {
                Console.WriteLine($"[Error]: username and password must contain at least {MIN_CHAR_COUNT} characters");
                continue;
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

        if (user != null)
        {
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