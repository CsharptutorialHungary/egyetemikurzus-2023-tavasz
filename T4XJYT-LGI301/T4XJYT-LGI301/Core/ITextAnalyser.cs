using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301.Core
{
	interface ITextAnalyser
	{
        string RawText { get; }

        int CountWords();

        int MaximumWordLength();

        int MinimumWordLength();

        double AverageWordLength();

        char MostCommonLetter();

        double WordDensity();

        List<Word> CreateWordsFromRawText();
    }
}

