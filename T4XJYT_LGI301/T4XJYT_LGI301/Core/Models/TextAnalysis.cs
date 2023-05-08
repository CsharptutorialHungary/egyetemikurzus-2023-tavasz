using System;

namespace T4XJYT_LGI301.Core.Models
{
    public record TextAnalysis
    {
        public int WordsCount { get; set; }

        public int MaximumWordLength { get; set; }

        public int MinimumWordLength { get; set; }

        public double AverageWordLength { get; set; }

        public string MostCommonLetter { get; set; }
        
        public string LongestWords { get; set; }

        public double WordDensity { get; set; }
    }
}

