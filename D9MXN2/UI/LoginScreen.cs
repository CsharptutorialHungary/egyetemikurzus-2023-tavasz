using D9MXN2.DataAccess.Actions;

namespace D9MXN2.UI;

public class LoginScreen: CredentialHandler
{
    public override void Render()
    {
        Console.WriteLine("Login\n");
        this.CredentialHandling(UserActions.Login);
    }
}