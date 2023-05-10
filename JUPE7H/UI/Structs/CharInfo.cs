namespace JUPE7H.UI.Structs;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
internal readonly struct CharInfo{
    [FieldOffset(0)]
    public readonly ushort Char;
    
    [FieldOffset(2)]
    public readonly short Attributes;

    public CharInfo(ushort c, short attributes){
        Char = c;
        Attributes = attributes;
    }

    public CharInfo(ushort c, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black){
        Char = c;
        Attributes = (short)((int)background * 16 + (int)foreground);
    }
}