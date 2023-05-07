using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D9MXN2.UI;

public class MainScreen : IScreen
{
    static Dictionary<string, Delegate> commands = new() {
        {"notes", () => System.Console.WriteLine()},
        {"add", () => System.Console.WriteLine()},
        {"delete", () => System.Console.WriteLine()},
        {"dump", () => System.Console.WriteLine()},
        {"load", () => System.Console.WriteLine()},
        {"logout", () => System.Console.WriteLine()},
    };

    public void Render()
    {
        throw new NotImplementedException();
    }
}