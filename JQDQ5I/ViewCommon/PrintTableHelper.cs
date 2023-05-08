namespace GoogleBooks.ViewCommon;
public static class PrintTableHelper
{
    const int _maxColumnSize = 50;
    static int[] _columnWidths = new int[10];

    public static void PrintTableFromList(List<List<string>> stringList)
    {
        _columnWidths = CalculateColumnWidths(stringList);
        int totalWith = _columnWidths.Sum() + _columnWidths.Count() + 1;

        stringList.ForEach(row => PrintTableRow(totalWith, row));
        PrintLine(totalWith);
    }

    private static void PrintTableRow(int totalWith, List<string> row)
    {
        PrintLine(totalWith);
        PrintRow(row);
    }

    private static void PrintLine(int width) => Console.WriteLine(new string('-', width));


    private static void PrintRow(List<string> row)
    {
        string currentRow = "|";
        for (int i = 0; i < row.Count; i++)
        {
            string element = row[i];
            int columnWidth = _columnWidths[i];
            currentRow += AlignCentre(element, columnWidth) + "|";
        }
        Console.WriteLine(currentRow);
    }

    private static string AlignCentre(string text, int columnWidths)
    {
        if (text.Length > _maxColumnSize)
            text = text.Substring(0, columnWidths - 3) + "...";

        return text.PadRight(((columnWidths - text.Length) / 2) + text.Length).PadLeft(columnWidths);

    }

    private static int[] CalculateColumnWidths(List<List<string>> rows)
    {
        int[] columnWidths = Enumerable.Range(0, rows[0].Count)
            .Select(j => Math.Min(rows.Max(row => row[j].Length), _maxColumnSize))
            .ToArray();
        return columnWidths;
    }


}
