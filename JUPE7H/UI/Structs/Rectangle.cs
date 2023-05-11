namespace JUPE7H.UI.Structs;

internal struct Rectangle{
    public short Left;
    public short Top;
    public short Right;
    public short Bottom;

    public Rectangle(short left, short top, short right, short bottom){
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }
}