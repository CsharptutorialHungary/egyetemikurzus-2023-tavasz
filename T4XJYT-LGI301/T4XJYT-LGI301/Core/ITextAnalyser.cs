using System;
using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301.Core
{
	public interface ITextAnalyser
	{
        string TextToAnalyze { get; set; }

        Word[] Words { get; set; }

        int CountWords();

        int MaximumWordLength();

        int MinimumWordLength();

        double AverageWordLength();

        char MostCommonLetter();

        double WordDensity();
    }
}

