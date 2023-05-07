using NA0K08_GK10ZO;
using NA0K08_GK10ZO.Model;
using System.Collections;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        string fileName = "2022.csv";
        string path = Path.Combine(Environment.CurrentDirectory, fileName);

        var races = new List<RaceResult>();

        using (var reader = new StreamReader(path))
        {
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                string country = values[0];
                int position = values[1] == "NC" ? 0 : Convert.ToInt32(values[1]);
                int driverNumber = Convert.ToInt32(values[2]);
                string driverName = values[3];
                string teamName = values[4];
                string startedFromPosition = values[5];
                string numberOfLaps = values[6];
                int points = Convert.ToInt32(values[8]);
                bool gotFastestLap = values[9] == "Yes" ? true : false;
                string fastestLap = values[10];

                var race = new RaceResult(country,position,driverNumber,driverName,teamName,
                    startedFromPosition,numberOfLaps,points,gotFastestLap,fastestLap);

                races.Add(race);
            }
        }

        var topThree = Stats.getTop3Winners(races);

        Console.WriteLine();
    }
}