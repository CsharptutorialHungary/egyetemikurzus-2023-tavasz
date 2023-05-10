﻿namespace JUPE7H.Logic;

using System.Text;
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
    public sbyte this[int x, int y] => VisibleTiles[x, y];
    public int RemainingMines => MineCount - FlagCount;

    private readonly int Seed;
    private readonly int MineCount;
    private readonly sbyte[,] RealTiles;
    private readonly sbyte[,] VisibleTiles;
    private bool IsGenerated;
    private int FlagCount;
    private int RevealedCount;

    private readonly BoardFile _boardFile;

    public Board(Point size, Difficulty difficulty, int? seed = null){
        Difficulty = difficulty;
        Width = size.X;
        Height = size.Y;
        Status = GameStatus.Running;

        Seed = seed ?? Guid.NewGuid().GetHashCode();
        MineCount = (int)(size.X * size.Y * (int)difficulty / 1000.0);
        RealTiles = new sbyte[size.X, size.Y];
        VisibleTiles = new sbyte[size.X, size.Y];
        IsGenerated = false;
        FlagCount = 0;
        RevealedCount = 0;
        
        _boardFile = new BoardFile(new Point(Width, Height), Difficulty, Seed);

        VisibleTiles.Fill(UNMARKED);
    }

    private void Generate(Point? safePosition = null){
        IsGenerated = true;
        Random random = new(Seed);
        int placedMines = 0;

        while(placedMines < MineCount){
            int x = random.Next() % Width;
            int y = random.Next() % Height;
            
            if(RealTiles[x, y] == MINE) continue;
            if(x == safePosition?.X && y == safePosition?.Y) continue;
            
            RealTiles[x, y] = MINE;
            placedMines++;
        }

        for(int x = 0; x < Width; x++){
            for(int y = 0; y < Height; y++){
                VisibleTiles[x, y] = UNMARKED;
                
                if(RealTiles[x, y] == MINE) continue;
                
                RealTiles[x, y] = (sbyte)RealTiles.AdjacentTo(x, y).Count(tile => tile == MINE);
            }
        }
    }

    public void Flag(Point position){
        if(Status != GameStatus.Running) return;
        if(!IsGenerated) Generate();
        
        _boardFile.Actions.Add(new BoardAction(position, true));
        
        sbyte selectedTile = VisibleTiles[position.X, position.Y];

        switch (selectedTile){
            case FLAG:
                VisibleTiles[position.X, position.Y] = UNMARKED;
                FlagCount--;
                break;
            case UNMARKED:
                VisibleTiles[position.X, position.Y] = FLAG;
                FlagCount++;
                break;
        }
        
        CheckWin();
    }

    public void Reveal(Point position, bool isChained = false){
        if(Status != GameStatus.Running) return;
        if(!IsGenerated) Generate(position);

        if(VisibleTiles[position.X, position.Y] != UNMARKED) return;
        
        if(!isChained) _boardFile.Actions.Add(new BoardAction(position, false));
        
        sbyte realValue = RealTiles[position.X, position.Y];

        if(realValue == MINE){
            Status = GameStatus.Lost;
            RealTiles.CopyTo(VisibleTiles);
            return;
        }
        
        VisibleTiles[position.X, position.Y] = realValue;
        RevealedCount++;
        if(realValue == 0){
            foreach((int x, int y) adjacent in RealTiles.AdjacentPositions(position.X, position.Y)){
                Reveal(new Point((short)adjacent.x, (short)adjacent.y), true);
            }
        }
        
        CheckWin();
    }

    private void CheckWin(){
        if(RemainingMines != 0) return;
        if(RevealedCount != Width * Height - MineCount) return;
        Status = GameStatus.Won;
    }

    public async Task SaveGame(string path) {
        string contents = JsonSerializer.Serialize(_boardFile);
        await File.WriteAllTextAsync(path, contents);
    }

    public static async Task<Board> LoadGame(string path) {
        string contents = await File.ReadAllTextAsync(path);

        //JsonElement? json = JsonSerializer.Deserialize<JsonElement>(contents);
        
        //if(json == null) throw new Exception("Failed to parse save file");

        BoardFile? boardFile = JsonSerializer.Deserialize<BoardFile>(contents); //json.Value.Deserialize<BoardFile>();
        
        if(boardFile == null) throw new Exception("Failed to parse save file");

        //boardFile.Actions = json.Value.GetProperty("Actions").Deserialize<BoardAction[]>()!.ToList();
        
        File.WriteAllText("last.txt", JsonSerializer.Serialize(boardFile));

        Board board = new(boardFile.Size, boardFile.Difficulty, boardFile.Seed);

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