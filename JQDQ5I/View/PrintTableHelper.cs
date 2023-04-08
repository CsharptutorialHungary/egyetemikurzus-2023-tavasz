namespace GoogleBooks.View;
public static class PrintTableHelper
{
    const int _maxColumnSize = 50;

    public static void PrintTableFromList(List<List<string>> stringList)
    {
        int[] columnWidths = CalculateColumnWidths(stringList);
        int totalWith = columnWidths.Sum() + columnWidths.Count() + 1;

        foreach (var row in stringList)
            PrintTableRow(columnWidths, totalWith, row);
        PrintLine(totalWith);
    }

    static void PrintTableRow(int[] columnWidths, int totalWith, List<string> row)
    {
        PrintLine(totalWith);
        PrintRow(columnWidths, row);
    }

    static void PrintLine(int width) => Console.WriteLine(new string('-', width));


    static void PrintRow(int[] columnWidths, List<string> row)
    {
        string currentRow = "|";
        for (int i = 0; i < row.Count; i++)
        {
            string element = row[i];
            int columnWidth = columnWidths[i];
            currentRow += AlignCentre(element, columnWidth) + "|";
        }
        Console.WriteLine(currentRow);
    }

    static string AlignCentre(string text, int columnWidths)
    {
        text = text.Length > _maxColumnSize ? text.Substring(0, columnWidths - 3) + "..." : text;

        return text.PadRight(columnWidths - (columnWidths - text.Length) / 2).PadLeft(columnWidths);

    }

    static int[] CalculateColumnWidths(List<List<string>> rows)
    {
        int[] columnWidths = Enumerable.Range(0, rows[0].Count)
            .Select(j => Math.Min(rows.Max(row => row[j].Length), _maxColumnSize))
            .ToArray();
        return columnWidths;
    }


}
