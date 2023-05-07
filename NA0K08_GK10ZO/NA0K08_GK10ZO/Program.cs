using NA0K08_GK10ZO;

internal class Program
{
    private static async Task Main(string[] args)
    {
        string path = $@"..\..\..\sources\2022.csv";

        try
        {
            var races = FileManager.GetRaceDataFromCsv(path);
            var topThree = await Stats.getTop3WinnersAsync(races);
            FileManager.SaveToJSON(topThree, "topThree.json");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured while reading data from file: {path}", ex.ToString());
        }

        Console.WriteLine();
    }
}