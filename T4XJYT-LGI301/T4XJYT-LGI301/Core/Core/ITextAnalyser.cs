using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301.Core
{
	interface ITextAnalyser
	{
        int CountWords();

        int MaximumWordLength();

        int MinimumWordLength();

        double AverageWordLength();

        string MostCommonLetter();

        double WordDensity();

        List<T> CreateWordsFromRawText<T>() where T : Word, new();

        TextAnalysis CreateTextAnalysis();
	}
}

