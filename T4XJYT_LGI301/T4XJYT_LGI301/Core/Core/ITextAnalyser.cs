using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301.Core.Core
{
	internal interface ITextAnalyser
	{
        int CountWords();

        int MaximumWordLength();

        int MinimumWordLength();

        double AverageWordLength();

        string MostCommonLetter();

        string LongestWords();
        
        double WordDensity();

        List<T> CreateWordsFromRawText<T>() where T : Word, new();

        TextAnalysis CreateTextAnalysis();
	}
}

