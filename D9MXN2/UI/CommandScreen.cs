namespace D9MXN2.UI;

public abstract class CommandScreen : BaseScreen
{
    protected abstract Dictionary<string, Delegate> Commands { get; set; }

    protected virtual bool SuccessfulCommandInvoke(string command)
    {
        Commands[command].DynamicInvoke();
        return true;
    }

    public override void Render(string? msg = default)
    {
        while (true)
        {
            if (msg != null) { Console.WriteLine(msg); }

            string user_command = GetChosenCommandFrom(this.Commands.Keys);
            if (!Commands.ContainsKey(user_command))
            {
                Console.WriteLine($"[Error]: There is no command called *{user_command}*\n");
                continue;
            }

            if (!SuccessfulCommandInvoke(user_command)) { break; }
        }
    }
}