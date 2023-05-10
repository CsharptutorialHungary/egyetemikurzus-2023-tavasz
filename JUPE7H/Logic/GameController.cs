namespace JUPE7H.Logic;

using UI.Elements;
using UI.Structs;
using static ConsoleColor;

internal sealed class GameController {
    private static readonly Point CANVAS_SIZE = new(60, 30);
    private static readonly Point BOARD_SIZE = new(40, 20);

    private readonly Canvas _canvas;
    private Point _cursorPosition;
    private bool _isBoardFocused;
    private Board _board;
    
    // input
    private readonly List<char> _inputCharacters;
    private short _inputOffset;
    private string _inputMessage;
    private bool _isMessageValid;

    public bool ShouldExit { get; private set; }

    public GameController() {
        _canvas = new Canvas(CANVAS_SIZE.X, CANVAS_SIZE.Y);
        _cursorPosition = new Point(0, 0);
        _isBoardFocused = true;
        ShouldExit = false;
        _inputCharacters = new List<char>();
        _inputOffset = 0;
        _inputMessage = "";
        _isMessageValid = true;

        NewGame();

        UpdateCanvas();
    }

    public void InputKey(ConsoleKeyInfo key) {
        if(_isBoardFocused) {
            InputToBoard(key);
        }
        else {
            InputToCommand(key);
        }

        UpdateCanvas();
    }

    private void InputToBoard(ConsoleKeyInfo key) {
        switch(key.Key) {
            case ConsoleKey.LeftArrow:
                if(_cursorPosition.X > 0) _cursorPosition.X--;
                break;

            case ConsoleKey.RightArrow:
                if(_cursorPosition.X < _board.Width - 1) _cursorPosition.X++;
                break;

            case ConsoleKey.UpArrow:
                if(_cursorPosition.Y > 0) _cursorPosition.Y--;
                break;

            case ConsoleKey.DownArrow:
                if(_cursorPosition.Y < _board.Height - 1) _cursorPosition.Y++;
                break;

            case ConsoleKey.F:
                _board.Flag(_cursorPosition);
                break;

            case ConsoleKey.R:
                _board.Reveal(_cursorPosition);
                break;
            case ConsoleKey.Tab:
                _isBoardFocused = false;
                break;
            case ConsoleKey.K: {
                if(_board.Status != GameStatus.Running) NewGame();
                break;
            }
        }
    }

    private void NewGame() {
        _board = new Board(BOARD_SIZE, Difficulty.Special);
    }

    private void InputToCommand(ConsoleKeyInfo key) {
        switch(key.Key) {
            case ConsoleKey.LeftArrow:
                if(_inputOffset > 0) _inputOffset--;
                break;

            case ConsoleKey.RightArrow:
                if(_inputOffset < _inputCharacters.Count) _inputOffset++;
                break;
            case ConsoleKey.Backspace:
                if(_inputOffset > 0) {
                    _inputCharacters.RemoveAt(_inputOffset - 1);
                    _inputOffset--;
                }
                break;
            case ConsoleKey.Enter:
                SendCommand(string.Join("", _inputCharacters));
                _inputCharacters.Clear();
                _inputOffset = 0;
                break;
            case ConsoleKey.Tab:
                _isBoardFocused = true;
                break;
            default:
                if(key.KeyChar < ' ' || key.KeyChar >= 127) break;
                _inputCharacters.Insert(_inputOffset, key.KeyChar);
                _inputOffset++;
                break;
        }
    }

    private void UpdateCanvas() {
        _canvas.Clear();

        for(int y = 0; y < _board.Height; y++) {
            for(int x = 0; x < _board.Width; x++) {
                sbyte value = _board[x, y];
                CharInfo c = value switch {
                    Board.MINE => new CharInfo('×', DarkRed),
                    Board.UNMARKED => new CharInfo('?', DarkGray),
                    Board.FLAG => new CharInfo('!', DarkRed),
                    0 => new CharInfo(' '),
                    _ => new CharInfo((char)('0' + value), CellColor(value))
                };

                _canvas[x, y] = c;
            }
        }

        if(_isBoardFocused) _canvas[_cursorPosition.X, _cursorPosition.Y] = new CharInfo(_canvas[_cursorPosition.X, _cursorPosition.Y].Char, White, DarkBlue);

        string statusMessage = _board.Status switch {
            GameStatus.Lost => "You lost! Press K to restart",
            GameStatus.Won => "You won! Press K to restart",
            _ => ""
        };

        ConsoleColor statusColor = _board.Status switch {
            GameStatus.Lost => DarkRed,
            GameStatus.Won => DarkGreen,
            _ => Gray
        };

        _canvas.PutString(0, 21, $"[{_board.RemainingMines}] mines, [{_board.Difficulty}] {statusMessage}", statusColor);


        _canvas.PutString(0, 23, $"{string.Join("", _inputCharacters)}", White, _isBoardFocused ? Black : Blue);
        if(!_isBoardFocused) _canvas[_inputOffset, 23] = new CharInfo(_canvas[_inputOffset, 23].Char, White, DarkBlue);

        _canvas.PutString(0, 25, _inputMessage, _isMessageValid ? Green : Red, Black, true);

        _canvas.Update();
    }

    private static ConsoleColor CellColor(int i) {
        return i switch {
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

    private void SendCommand(string value) {
        string[] args = value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if(args.Length == 0) return;

        switch(args[0]) {
            case "save":
                if(args.Length != 2) {
                    _isMessageValid = false;
                    _inputMessage = $"Command expects 1 argument (got {args.Length - 1})";
                    break;
                }

                try {
                    Task saveTask = _board.SaveGame($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{args[1]}.json");
                    saveTask.Wait();
                    
                    _isMessageValid = true;
                    _inputMessage = "Successfully saved!";
                }
                catch(Exception e) {
                    _isMessageValid = false;
                    _inputMessage = e.Message;
                }
                
                break;
            case "load":
                if(args.Length != 2) {
                    _isMessageValid = false;
                    _inputMessage = $"Command expects 1 argument (got {args.Length - 1})";
                    break;
                }
                
                try {
                    Task<Board> saveTask = Board.LoadGame($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{args[1]}.json");
                    saveTask.Wait();

                    _board = saveTask.Result;
                    
                    _isMessageValid = true;
                    _inputMessage = "Successfully loaded!";
                }
                catch(Exception e) {
                    _isMessageValid = false;
                    _inputMessage = e.Message;
                }
                
                break;
            default:
                _isMessageValid = false;
                _inputMessage = $"Command {args[0]} does not exist";
                break;
        }
    }  
}
