namespace JUPE7H.Logic;

using System.Text.Json;
using UI.Structs;

internal sealed class Board{
    public const sbyte MINE = -1;
    public const sbyte FLAG = -2;
    public const sbyte UNMARKED = 127;
    
    public readonly Difficulty Difficulty;
    public readonly short Width;
    public readonly short Height;
    public GameStatus Status{ get; private set; }
    public sbyte this[int x, int y] => _visibleTiles[x, y];
    public int RemainingMines => _mineCount - _flagCount;

    private readonly int _seed;
    private readonly int _mineCount;
    private readonly sbyte[,] _realTiles;
    private readonly sbyte[,] _visibleTiles;
    private bool _isGenerated;
    private int _flagCount;
    private int _revealedCount;

    private readonly SaveFile _saveFile;

    public Board(Point size, Difficulty difficulty, int? seed = null){
        Difficulty = difficulty;
        Width = size.X;
        Height = size.Y;
        Status = GameStatus.InProgress;

        _seed = seed ?? Guid.NewGuid().GetHashCode();
        _mineCount = (int)(size.X * size.Y * (int)difficulty / 1000.0);
        _realTiles = new sbyte[size.X, size.Y];
        _visibleTiles = new sbyte[size.X, size.Y];
        _isGenerated = false;
        _flagCount = 0;
        _revealedCount = 0;
        
        _saveFile = new SaveFile(new Point(Width, Height), Difficulty, _seed);

        _visibleTiles.Fill(UNMARKED);
    }

    private void Generate(Point? safePosition = null){
        _isGenerated = true;
        Random random = new(_seed);
        int placedMines = 0;

        while(placedMines < _mineCount){
            int x = random.Next() % Width;
            int y = random.Next() % Height;
            
            if(_realTiles[x, y] == MINE) continue;
            if(x == safePosition?.X && y == safePosition?.Y) continue;
            
            _realTiles[x, y] = MINE;
            placedMines++;
        }

        for(int x = 0; x < Width; x++){
            for(int y = 0; y < Height; y++){
                _visibleTiles[x, y] = UNMARKED;
                
                if(_realTiles[x, y] == MINE) continue;
                
                _realTiles[x, y] = (sbyte)_realTiles.AdjacentTo(x, y).Count(tile => tile == MINE);
            }
        }
    }

    public void Flag(Point position){
        if(Status != GameStatus.InProgress) return;
        if(!_isGenerated) Generate();
        
        _saveFile.Actions.Add(new BoardAction(position, true));
        
        sbyte selectedTile = _visibleTiles[position.X, position.Y];

        switch (selectedTile){
            case FLAG:
                _visibleTiles[position.X, position.Y] = UNMARKED;
                _flagCount--;
                break;
            case UNMARKED:
                _visibleTiles[position.X, position.Y] = FLAG;
                _flagCount++;
                break;
        }
        
        CheckWin();
    }

    public void Reveal(Point position, bool isChained = false){
        if(Status != GameStatus.InProgress) return;
        if(!_isGenerated) Generate(position);

        if(_visibleTiles[position.X, position.Y] != UNMARKED) return;
        
        if(!isChained) _saveFile.Actions.Add(new BoardAction(position, false));
        
        sbyte realValue = _realTiles[position.X, position.Y];

        if(realValue == MINE){
            Status = GameStatus.Lost;
            _realTiles.CopyTo(_visibleTiles);
            return;
        }
        
        _visibleTiles[position.X, position.Y] = realValue;
        _revealedCount++;
        if(realValue == 0){
            foreach((int x, int y) adjacent in _realTiles.AdjacentPositions(position.X, position.Y)){
                Reveal(new Point((short)adjacent.x, (short)adjacent.y), true);
            }
        }
        
        CheckWin();
    }

    private void CheckWin(){
        if(RemainingMines != 0) return;
        if(_revealedCount != Width * Height - _mineCount) return;
        Status = GameStatus.Won;
    }

    public async Task SaveGame(string path) {
        string contents = JsonSerializer.Serialize(_saveFile);
        await File.WriteAllTextAsync(path, contents);
    }

    public static async Task<Board> LoadGame(string path) {
        string contents = await File.ReadAllTextAsync(path);

        SaveFile? boardFile = JsonSerializer.Deserialize<SaveFile>(contents);
        
        if(boardFile == null) throw new Exception("Failed to parse save file");

        Board board = new(boardFile.BoardSize, boardFile.Difficulty, boardFile.Seed);

        foreach(BoardAction action in boardFile.Actions) {
            if(action.IsFlag) {
                board.Flag(action.Location);
            }
            else {
                board.Reveal(action.Location);
            }
        }

        return board;
    }
}