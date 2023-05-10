namespace JUPE7H.UI.Elements;

using Microsoft.Win32.SafeHandles;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Structs;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal sealed class Canvas{
    #region DLL import

    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteConsoleOutputW(
        SafeFileHandle hConsoleOutput,
        CharInfo[] lpBuffer,
        Point dwBufferSize,
        Point dwBufferCoord,
        ref Rectangle lpWriteRegion);

    #endregion

    private SafeFileHandle _handle{ get; }
    private CharInfo[] _buffer{ get; set; }
    private Point _size{ get; set; }
    
    public short Width => _size.X;
    public short Height => _size.Y;

    public Canvas(short width, short height){
        _handle = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        _size = new Point(width, height);
        _buffer = Array.Empty<CharInfo>();
        
        Resize(width, height);

        if(_handle.IsInvalid) throw new ApplicationException("Failed to get console handle");
        if(!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) throw new ApplicationException("This application is only supported on windows");
    }

    #region Buffer manipulation

    public void Clear(){
        this[.., ..] = new CharInfo(' ');
    }
    
    public void Resize(short width, short height, bool keepData = false){
        Console.SetWindowSize(width, height);
        
        Point newSize = new(width, height);
        CharInfo[] newBuffer = new CharInfo[Width * Height];

        if(keepData){
            short xMax = Math.Max(newSize.X, _size.X);
            short yMax = Math.Max(newSize.Y, _size.Y);

            for(short y = 0; y < yMax; y++){
                for(short x = 0; x < xMax; x++){
                    newBuffer[y * newSize.X + x] = _buffer[y * _size.X + x];
                }
            }
        }
            
        _size = newSize;
        _buffer = newBuffer;
        
        if(!keepData){
            Clear();
        }
    }

    public void PutString(int x, int y, string value, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black, bool canWrap = false){
        int index = 0;
        
        for(int i = x; (canWrap || i < Width) && Width * y + i < _buffer.Length && index < value.Length; i++){
            _buffer[Width * y + i] = new CharInfo(value[index++], foreground, background);
        }
    }

    public CharInfo this[int x, int y]{
        get => _buffer[y * Width + x];
        set => _buffer[y * Width + x] = value;
    }
    
    public CharInfo this[Range x, Range y]{
        set{
            short startX = (short)(x.Start.IsFromEnd ? Width - x.Start.Value : x.Start.Value);
            short endX = (short)(x.End.IsFromEnd ? Width - x.End.Value : x.End.Value);
            short startY = (short)(y.Start.IsFromEnd ? Height - y.Start.Value : y.Start.Value);
            short endY = (short)(y.End.IsFromEnd ? Height - y.End.Value : y.End.Value);
            
            for(int py = startY; py < endY; py++){
                for(int px = startX; px < endX; px++){
                    int index = Width * py + px;
                    _buffer[index] = value;
                }
            }
        }
    }
    
    #endregion
    
    public void Update(Rectangle? area = null){
        Rectangle writeRegion = area ?? new Rectangle{Left = 0, Top = 0, Right = Width, Bottom = Height};
        
        WriteConsoleOutputW(
            _handle,
            _buffer,
            new Point{X = Width, Y = Height},
            new Point{X = 0, Y = 0},
            ref writeRegion
            );
    }
}