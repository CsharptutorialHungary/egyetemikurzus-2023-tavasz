namespace JUPE7H.Logic;

using UI.Elements;
using UI.Structs;

internal sealed class GameController{
    private readonly Canvas _canvas;
    private Point _cursorPosition;

    public bool ShouldExit{ get; private set; }

    public GameController(Canvas canvas){
        _canvas = canvas;
        _cursorPosition = new Point(0, 0);

        ShouldExit = false;

        UpdateCanvas();
    }

    public void InputKey(ConsoleKeyInfo key){
        HandleKey(key);
        UpdateCanvas();
    }

    private void HandleKey(ConsoleKeyInfo key){
        switch(key.Key){
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
        }
    }

    private void UpdateCanvas(){
        _canvas.Clear();
        _canvas[_cursorPosition.X, _cursorPosition.Y] = new CharInfo('X');
        _canvas.Update();
    }

    /*ConsoleColor CellColor(int i){
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
       }*/
}
