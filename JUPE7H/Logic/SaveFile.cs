namespace JUPE7H.Logic;

using UI.Structs;

internal sealed class SaveFile {
    public List<BoardAction> Actions { get; set; }
    public Point BoardSize { get; }
    public Difficulty Difficulty { get; }
    public int Seed { get; }

    public SaveFile(Point boardSize, Difficulty difficulty, int seed) {
        Actions = new List<BoardAction>();
        BoardSize = boardSize;
        Difficulty = difficulty;
        Seed = seed;
    }
}
