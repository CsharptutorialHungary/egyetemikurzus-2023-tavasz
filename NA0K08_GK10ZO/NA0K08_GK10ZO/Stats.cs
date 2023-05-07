using NA0K08_GK10ZO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NA0K08_GK10ZO
{
    internal class Stats
    {

        public static List<List<string>> getTop3Winners(List<RaceResult> raceResults)
        {

            List<string> results = new List<string>();
            Console.WriteLine();
            var result = raceResults
                .Where(m => m.Position != 0)
                .GroupBy(m => m.Country)
                .OrderBy(g => g.Min(m => m.Position))
                .Select(g => g.Select(m => m.DriverName).ToList())
                .ToList();

            return result;

        }

    }
}
