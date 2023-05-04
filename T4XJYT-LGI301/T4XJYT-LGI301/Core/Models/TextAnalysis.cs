using System;

namespace T4XJYT_LGI301.Core.Models
{
    record TextAnalysis
    {
        public int WordsCount { get; set; }

        public int MaximumWordLength { get; set; }

        public int MinimumWordLength { get; set; }

        public int AverageWordLength { get; set; }

        public char MostCommonLetter { get; set; }

        public double WordDensity { get; set; }
    }
}

