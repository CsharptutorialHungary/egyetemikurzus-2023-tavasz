namespace JUPE7H.Logic;

internal static class Array2DExtensions{
    public static void Fill<T>(this T[,] array, T value) where T: struct{
        for(int x = 0; x < array.GetLength(0); x++){
            for(int y = 0; y < array.GetLength(1); y++){
                array[x, y] = value;
            }
        }
    }
    
    public static void CopyTo<T>(this T[,] source, T[,] destination) where T: struct{
        if(source.GetLength(0) != destination.GetLength(0) || source.GetLength(1) != destination.GetLength(1)){
            throw new IndexOutOfRangeException("Source and destination sizes must match");
        }
        
        for(int x = 0; x < source.GetLength(0); x++){
            for(int y = 0; y < source.GetLength(1); y++){
                destination[x, y] = source[x, y];
            }
        }
    }

    public static T[] AdjacentTo<T>(this T[,] array, int x, int y) where T: struct{
        int width = array.GetLength(0), height = array.GetLength(1);
        
        if(x < 0 || x >= width || y < 0 || y >= height){
            throw new IndexOutOfRangeException($"params: ({x}, {y}), size: ({width}, {height})");
        }

        int dxStart = x == 0 ? 0 : -1;
        int dxEnd = x == width - 1 ? 0 : 1;
        int dyStart = y == 0 ? 0 : -1;
        int dyEnd = y == height - 1 ? 0 : 1;

        int index = 0;
        T[] results = new T[(dxEnd - dxStart + 1) * (dyEnd - dyStart + 1) - 1];
        
        for(int dx = dxStart; dx <= dxEnd; dx++){
            for(int dy = dyStart; dy <= dyEnd; dy++){
                if(dx == 0 && dy == 0) continue;
                results[index++] = array[x + dx, y + dy];
            }
        }

        return results;
    }
    
    public static (int x, int y)[] AdjacentPositions<T>(this T[,] array, int x, int y) where T: struct{
        int width = array.GetLength(0), height = array.GetLength(1);
        
        if(x < 0 || x >= width || y < 0 || y >= height){
            throw new IndexOutOfRangeException($"params: ({x}, {y}), size: ({width}, {height})");
        }

        int dxStart = x == 0 ? 0 : -1;
        int dxEnd = x == width - 1 ? 0 : 1;
        int dyStart = y == 0 ? 0 : -1;
        int dyEnd = y == height - 1 ? 0 : 1;

        int index = 0;
        (int, int)[] results = new (int, int)[(dxEnd - dxStart + 1) * (dyEnd - dyStart + 1) - 1];
        
        for(int dx = dxStart; dx <= dxEnd; dx++){
            for(int dy = dyStart; dy <= dyEnd; dy++){
                if(dx == 0 && dy == 0) continue;
                results[index++] = (x + dx, y + dy);
            }
        }

        return results;
    }
}