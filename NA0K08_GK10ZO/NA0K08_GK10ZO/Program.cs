using NA0K08_GK10ZO;

internal class Program
{
    private static void Main(string[] args)
    {
        string fileName = "2022.csv";
        string path = Path.Combine(Environment.CurrentDirectory, fileName);

        try
        {
            var races = FileManager.GetRaceDataFromCsv(path);
            var topThree = Stats.getTop3Winners(races);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured while reading data from file: {path}", ex.ToString());
        }

        Console.WriteLine();
    }
}