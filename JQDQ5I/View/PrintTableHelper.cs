using System.ComponentModel;

namespace GoogleBooks.View;
public static class PrintTableHelper
{
    public static void PrintTableFromList<T>(List<T> rows) where T : class
    {
        List<List<string>> stringList = ConvertClassListToStringList(rows);
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
        text = text.Length > columnWidths ? text.Substring(0, columnWidths - 3) + "..." : text;

        if (string.IsNullOrEmpty(text))
        {
            return new string(' ', columnWidths);
        }
        else
        {
            return text.PadRight(columnWidths - (columnWidths - text.Length) / 2).PadLeft(columnWidths);
        }
    }

    static int[] CalculateColumnWidths(List<List<string>> rows)
    {
        int[] columnWidths = new int[rows[0].Count];
        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < rows[i].Count; j++)
            {
                int length = rows[i][j].Length;
                if (length > columnWidths[j])
                {
                    columnWidths[j] = length;
                }
            }
        }
        return columnWidths;
    }

    static List<List<string>> ConvertClassListToStringList<T>(List<T> classList) where T : class
    {
        List<List<string>> stringList = new List<List<string>>();
        List<string> header = new List<string>();

        int propertyCount = classList[0].GetType().GetProperties().Length;

        foreach (T obj in classList)
        {
            List<string> row = new List<string>();

            foreach (var prop in obj.GetType().GetProperties())
            {
                if (header.Count < propertyCount)
                {
                    var displayNameAttribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as DisplayNameAttribute;
                    header.Add(displayNameAttribute?.DisplayName);
                }

                if (prop.GetValue(obj, null) != null)
                    row.Add(prop.GetValue(obj, null).ToString());
                else
                    row.Add("-");
            }
            stringList.Add(row);
        }

        stringList.Insert(0, header);
        return stringList;
    }
}