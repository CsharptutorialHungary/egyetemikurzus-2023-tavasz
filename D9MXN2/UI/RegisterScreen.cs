using D9MXN2.DataAccess.Actions;

namespace D9MXN2.UI;

public class RegisterScreen : CredentialHandler
{
    public override void Render()
    {
        Console.WriteLine("Register\n");
        this.CredentialHandling(UserActions.Register);
    }
}