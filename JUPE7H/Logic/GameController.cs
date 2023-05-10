namespace JUPE7H.Logic;

using System.Reflection;
using UI.Elements;
using UI.Structs;
using static ConsoleColor;

internal sealed class GameController {
    private readonly MethodInfo[] _commands;
    private readonly Canvas _canvas;
    private Point _cursorPosition;
    private bool _boardFocused;
    private Board _board;

    public bool ShouldExit { get; private set; }

    public GameController(Canvas canvas) {
        _commands = typeof(GameController)
            .GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(x => x.GetCustomAttribute<CommandAttribute>() != null)
            .ToArray();
        
        _canvas = canvas;
        _cursorPosition = new Point(0, 0);
        _boardFocused = true;

        ShouldExit = false;

        New();

        UpdateCanvas();
    }

    public void InputKey(ConsoleKeyInfo key) {
        HandleKey(key);
        UpdateCanvas();
    }

    private void HandleKey(ConsoleKeyInfo key) {
        switch(key.Key) {
            case ConsoleKey.LeftArrow:
                if(_cursorPosition.X > 0) _cursorPosition.X--;
                break;

            case ConsoleKey.RightArrow:
                if(_cursorPosition.X < _canvas.Width - 1) _cursorPosition.X++;
                break;

            case ConsoleKey.UpArrow:
                if(_cursorPosition.Y > 0) _cursorPosition.Y--;
                break;

            case ConsoleKey.DownArrow:
                if(_cursorPosition.Y < _canvas.Height - 1) _cursorPosition.Y++;
                break;
                
            case ConsoleKey.F:
                _board.Flag(_cursorPosition);
                break;
                
            case ConsoleKey.R:
                _board.Reveal(_cursorPosition);
                break;
                
            /*case ConsoleKey.K:
                if(_board.Status != GameStatus.Running) _board = new Board(boardSize, difficulty);
                break;*/
        }
    }

    private void UpdateCanvas() {
        _canvas.Clear();
        
        for(int y = 0; y < _board.Height; y++){
            for(int x = 0; x < _board.Width; x++){
                sbyte value = _board[x, y];
                CharInfo c = value switch{
                    Board.MINE => new CharInfo('×', DarkRed),
                    Board.UNMARKED => new CharInfo('?', DarkGray),
                    Board.FLAG => new CharInfo('!', DarkRed),
                    0 => new CharInfo(' '),
                    _ => new CharInfo((char)('0' + value), CellColor(value))
                };
                    
                _canvas[x, y] = c;
            }
        }
        
        _canvas[_cursorPosition.X, _cursorPosition.Y] = new CharInfo(_canvas[_cursorPosition.X, _cursorPosition.Y].Char, White, DarkBlue);
        
        _canvas.Update();
    }

    private static ConsoleColor CellColor(int i){
           return i switch{
               1 => Blue,
               2 => Green,
               3 => Red,
               4 => DarkBlue,
               5 => DarkYellow,
               6 => Cyan,
               7 => White,
               8 => Yellow,
               _ => Black
           };
    }

    [Command]
    public void New(string diffidulty = "normal") {
        _board = new Board(new Point(20, 20), Difficulty.Special);
    }
}
