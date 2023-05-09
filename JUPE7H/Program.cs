namespace JUPE7H;

using UI.Elements;
using Logic;

internal static class Program{
    private static void Main(string[] _){
        Console.CursorVisible = false;

        Canvas canvas = new(10, 10);
        GameController gameController = new(canvas);

        while(!gameController.ShouldExit){
            ConsoleKeyInfo key = Console.ReadKey(true);
            gameController.InputKey(key);
        }
    }
}
