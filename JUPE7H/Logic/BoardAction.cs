namespace JUPE7H.Logic;

using UI.Structs;

internal struct BoardAction {
    public Point Location { get; set; }
    public bool IsFlag { get; set; }

    public BoardAction(Point location, bool isFlag) {
        Location = location;
        IsFlag = isFlag;
    }
}
