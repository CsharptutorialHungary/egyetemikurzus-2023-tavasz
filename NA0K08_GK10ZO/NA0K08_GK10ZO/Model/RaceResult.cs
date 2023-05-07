using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA0K08_GK10ZO.Model
{
    public class RaceResult
    {
        private string Country { get; set; }
        private int Position { get; set; }
        private int DriverNumber { get; set; }
        private string DriverName { get; set; }
        private string TeamName { get; set; }
        private string StartedFromPosition { get; set; }
        private string NumberOfLaps { get; set; }
        private int Points { get; set; }
        private bool GotFastestLap { get; set; }
        private string FastestLap { get; set; }



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
