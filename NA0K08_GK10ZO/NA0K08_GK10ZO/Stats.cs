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
        public static async Task<List<TopThree>> getTop3WinnersAsync(List<RaceResult> raceResults)
        {
            List<string> results = new List<string>();
            Console.WriteLine();

            var result = await Task.Run(() =>
                raceResults
                .Where(m => m.Position != 0)
                .GroupBy(m => m.Country)
                .OrderBy(g => g.Min(m => m.Position))
                .Select(g => g.Select(m => m.DriverName).ToList())
                .ToList()
            );

            var resultList = new List<TopThree>();

            foreach (var r in result)
            {
                var item = new TopThree(r[0].ToString(), r[1].ToString(), r[2].ToString());
                resultList.Add(item);
            }

            return resultList;
        }

    }
}
