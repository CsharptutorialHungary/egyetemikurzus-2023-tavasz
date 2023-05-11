namespace JUPE7H.Logic;

using UI.Elements;
using UI.Structs;
using static ConsoleColor;

internal sealed class GameController {
    private static readonly Point CANVAS_SIZE = new(90, 30);
    private static readonly Point BOARD_SIZE = new(40, 20);
    private static readonly string[] USAGE_TIPS = {
        "arrows to move on board",
        "F to flag a position",
        "R to reveal a position",
        "Tab to switch between board and commands",
        "save <path>: save game",
        "load <path>: load game",
        "new <difficulty>: start a new game"
    };

    private readonly Canvas _canvas;
    private Point _cursorPosition;
    private bool _isBoardFocused;
    private Board _board;

    private readonly List<char> _inputCharacters;
    private short _inputCursorPosition;
    private string _inputMessage;
    private bool _isInputValid;

    public bool ShouldExit { get; private set; }

    public GameController() {
        _canvas = new Canvas(CANVAS_SIZE.X, CANVAS_SIZE.Y);
        _cursorPosition = new Point(0, 0);
        _isBoardFocused = true;
        ShouldExit = false;
        _inputCharacters = new List<char>();
        _inputCursorPosition = 0;
        _inputMessage = "";
        _isInputValid = true;

        _board = new Board(BOARD_SIZE, Difficulty.Normal);

        UpdateUI();
    }

    public void HandleKey(ConsoleKeyInfo key) {
        Console.CursorVisible = false;
        
        if(_isBoardFocused) {
            InputToBoard(key);
        }
        else {
            InputToCommand(key);
        }

        UpdateUI();
    }

    private void InputToBoard(ConsoleKeyInfo key) {
        switch(key.Key) {
            case ConsoleKey.LeftArrow:
                _cursorPosition.X--;
                if(_cursorPosition.X < 0) _cursorPosition.X = (short)(_board.Width - 1);
                break;

            case ConsoleKey.RightArrow:
                _cursorPosition.X++;
                if(_cursorPosition.X >= _board.Width) _cursorPosition.X = 0;
                break;

            case ConsoleKey.UpArrow:
                _cursorPosition.Y--;
                if(_cursorPosition.Y < 0) _cursorPosition.Y = (short)(_board.Height - 1);
                break;

            case ConsoleKey.DownArrow:
                _cursorPosition.Y++;
                if(_cursorPosition.Y >= _board.Height) _cursorPosition.Y = 0;
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
        }
    }

    private void InputToCommand(ConsoleKeyInfo key) {
        switch(key.Key) {
            case ConsoleKey.LeftArrow:
                if(_inputCursorPosition > 0) _inputCursorPosition--;
                break;

            case ConsoleKey.RightArrow:
                if(_inputCursorPosition < _inputCharacters.Count) _inputCursorPosition++;
                break;
            case ConsoleKey.Backspace:
                if(_inputCursorPosition > 0) {
                    _inputCharacters.RemoveAt(_inputCursorPosition - 1);
                    _inputCursorPosition--;
                }
                break;
            case ConsoleKey.Delete:
                if(_inputCursorPosition < _inputCharacters.Count) {
                    _inputCharacters.RemoveAt(_inputCursorPosition);
                }
                break;
            case ConsoleKey.Enter:
                SendCommand(string.Join("", _inputCharacters));
                _inputCharacters.Clear();
                _inputCursorPosition = 0;
                break;
            case ConsoleKey.Tab:
                _isBoardFocused = true;
                _inputMessage = "";
                break;
            default:
                if(key.KeyChar < ' ' || key.KeyChar >= 127) break;
                _inputCharacters.Insert(_inputCursorPosition, key.KeyChar);
                _inputCursorPosition++;
                break;
        }
    }

    private void UpdateUI() {
        ConsoleColor CellColor(int i) {
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
            GameStatus.Lost => "You lost!",
            GameStatus.Won => "You won!",
            _ => ""
        };

        ConsoleColor statusColor = _board.Status switch {
            GameStatus.Lost => DarkRed,
            GameStatus.Won => DarkGreen,
            _ => Gray
        };

        _canvas.PutString(0, BOARD_SIZE.Y + 1, $"[{_board.RemainingMines}] mines, [{_board.Difficulty}]");
        _canvas.PutString(0, BOARD_SIZE.Y + 2, statusMessage, statusColor);
        
        _canvas.PutString(0, BOARD_SIZE.Y + 4, $"{string.Join("", _inputCharacters).PadRight(CANVAS_SIZE.X - 2)}", White, _isBoardFocused ? Black : DarkGray);
        if(!_isBoardFocused) _canvas[_inputCursorPosition, BOARD_SIZE.Y + 4] = new CharInfo(_canvas[_inputCursorPosition, BOARD_SIZE.Y + 4].Char, White, DarkBlue);

        _canvas.PutString(0, BOARD_SIZE.Y + 6, _inputMessage, _isInputValid ? Green : Red, Black, true);

        for(int i = 0; i < USAGE_TIPS.Length; i++) {
            _canvas.PutString(BOARD_SIZE.X + 3, 2 * i + 1, USAGE_TIPS[i]);
        }

        _canvas.Update();
    }

    private void SendCommand(string value) {
        string[] args = value.Split(' ').Select(x => x.Trim()).Where(x => x != string.Empty).ToArray();

        string? command = args.FirstOrDefault();
        
        if(command is null) return;

        switch(command) {
            case "save":
                if(args.Length != 2) {
                    _isInputValid = false;
                    _inputMessage = $"Command expects 1 argument (got {args.Length - 1})";
                    break;
                }

                try {
                    Task saveTask = _board.SaveGame($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{args[1]}.json");
                    saveTask.Wait();

                    _isInputValid = true;
                    _inputMessage = "Successfully saved!";
                }
                catch(Exception e) {
                    _isInputValid = false;
                    _inputMessage = e.Message;
                }

                break;
            case "load":
                if(args.Length != 2) {
                    _isInputValid = false;
                    _inputMessage = $"Command expects 1 argument (got {args.Length - 1})";
                    break;
                }

                try {
                    Task<Board> saveTask = Board.LoadGame($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{args[1]}.json");
                    saveTask.Wait();

                    _board = saveTask.Result;

                    _isInputValid = true;
                    _inputMessage = "Successfully loaded!";
                }
                catch(Exception e) {
                    _isInputValid = false;
                    _inputMessage = e.Message;
                }

                break;
            case "new":
                if(args.Length != 2) {
                    _isInputValid = false;
                    _inputMessage = $"Command expects 1 argument (got {args.Length - 1})";
                    break;
                }
                Difficulty? difficulty = args[1] switch {
                    "e" => Difficulty.Easy,
                    "easy" => Difficulty.Easy,
                    "n" => Difficulty.Normal,
                    "normal" => Difficulty.Normal,
                    "h" => Difficulty.Hard,
                    "hard" => Difficulty.Hard,
                    "x" => Difficulty.Extreme,
                    "extreme" => Difficulty.Extreme,
                    _ => null
                };

                if(difficulty is null) {
                    _isInputValid = false;
                    _inputMessage = $"'{args[1]}' is not a valid difficulty level";
                }
                else {
                    _board = new Board(BOARD_SIZE, difficulty.Value);
                }

                break;
            default:
                _isInputValid = false;
                _inputMessage = $"Command {command} does not exist";
                break;
        }
    }
}
