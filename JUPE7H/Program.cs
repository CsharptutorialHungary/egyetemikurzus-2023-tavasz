namespace JUPE7H;

using Logic;

internal static class Program{
    private static void Main(string[] _){
        Console.CursorVisible = false;

        GameController gameController = new();

        while(!gameController.ShouldExit){
            ConsoleKeyInfo key = Console.ReadKey(true);
            gameController.InputKey(key);
        }
    }
}
