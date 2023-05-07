using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA0K08_GK10ZO.Model
{
    internal class RaceResult
    {
        public string Country { get; set; }
        public int Position { get; set; }
        public int DriverNumber { get; set; }
        public string DriverName { get; set; }
        public string TeamName { get; set; }
        public string StartedFromPosition { get; set; }
        public string NumberOfLaps { get; set; }
        public int Points { get; set; }
        public bool GotFastestLap { get; set; }
        public string FastestLap { get; set; }

        public RaceResult(string country, int position, int driverNumber, string driverName, string teamName,
            string startedFromPosition, string numberOfLaps, int points, bool gotFastestLap, string fastestLap)
        {
            Country = country;
            Position = position;
            DriverNumber = driverNumber;
            DriverName = driverName;
            TeamName = teamName;
            StartedFromPosition = startedFromPosition;
            NumberOfLaps = numberOfLaps;
            Points = points;
            GotFastestLap = gotFastestLap;
            FastestLap = fastestLap;
        }

        public override string? ToString()
        {
            return @$"Country: {Country}, Position: {Position}, DriverNumber: {DriverNumber}, DriverName: {DriverName}
                    TeamName: {TeamName}, StartedFromPosition: {StartedFromPosition}, NumberOfLaps: {NumberOfLaps},
                    Points: {Points}, GotFastestLap: {GotFastestLap}, FastestLap: {FastestLap}";
        }
    }
}
