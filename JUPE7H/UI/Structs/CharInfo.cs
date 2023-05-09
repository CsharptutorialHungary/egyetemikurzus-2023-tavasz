namespace JUPE7H.UI.Structs;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
internal struct CharInfo{
    [FieldOffset(0)]
    public ushort Char;
    
    [FieldOffset(2)]
    public short Attributes;

    public CharInfo(ushort c, short attributes){
        Char = c;
        Attributes = attributes;
    }

    public CharInfo(ushort c, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black){
        Char = c;
        Attributes = (short)((int)background * 16 + (int)foreground);
    }
}