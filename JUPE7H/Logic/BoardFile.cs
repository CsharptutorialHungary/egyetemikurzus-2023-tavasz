namespace JUPE7H.Logic;

using UI.Structs;

internal sealed class BoardFile {
    public List<BoardAction> Actions { get; set; }
    public Point Size { get; }
    public Difficulty Difficulty { get; }
    public int Seed { get; }

    public BoardFile(Point size, Difficulty difficulty, int seed) {
        Actions = new List<BoardAction>();
        Size = size;
        Difficulty = difficulty;
        Seed = seed;
    }
}
