using System.Linq;
using System.Text.RegularExpressions;
using T4XJYT_LGI301.Core;
using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301
{
    public class TextAnalyser : ITextAnalyser
    {
        private readonly string _rawText;
        private readonly List<Word> _cleanedUpWords;
        public TextAnalyser(string textToAnalyze)
        {
            _rawText = textToAnalyze;
            _cleanedUpWords = CreateWordsFromRawText<Word>();
        }

        public int CountWords()
        {
            if (!_cleanedUpWords.Any()) return 0;

            int count = _cleanedUpWords
                .Count();

            return count;
        }

        public int MaximumWordLength()
        {
            if (!_cleanedUpWords.Any()) return 0;

            return _cleanedUpWords
                .Max(word => word.Text.Length);
        }

        public int MinimumWordLength()
        {
            if (!_cleanedUpWords.Any()) return 0;

            return _cleanedUpWords
                .Min(word => word.Text.Length);
        }

        public double AverageWordLength()
        {
            if (!_cleanedUpWords.Any()) return 0;

            double average = _cleanedUpWords
                .Select(word => word.Text.Length)
                .Average();

            return Math.Floor(average * 100) / 100;
        }

        public string MostCommonLetter()
        {
            if (!_cleanedUpWords.Any()) return "NA";

            return _cleanedUpWords
                .SelectMany(word => word.Text)
                .GroupBy(letter => letter)
                .OrderByDescending(group => group.Count())
                .First()
                .Key
                .ToString();
        }

        public double WordDensity()
        {
            if (!_cleanedUpWords.Any()) return 0;

            int totalWords = _cleanedUpWords.Count;
            int uniqueWords = _cleanedUpWords
                .Select(word => word.Text.ToLower())
                .Distinct()
                .Count();

            double density = totalWords != 0 ? (double)uniqueWords / totalWords : 0;
            return Math.Floor(density * 100) / 100;
        }

        public List<T> CreateWordsFromRawText<T>() where T : Word, new()
        {
            // Remove [" from the start and "] from the end of the text
            string trimmedText = _rawText.TrimStart('[').TrimEnd(']');

            // Remove every symbol such as .,!? etc.
            string symbols = @"[^a-zA-Z\s]";
            string cleanedText = Regex.Replace(trimmedText, symbols, "");

            // Split the text into words
            string[] words = cleanedText.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Create a list of words
            List<T> wordList = new List<T>();
            foreach (string word in words)
            {
                T newWord = new T { Text = word.ToLowerInvariant() };
                wordList.Add(newWord);
            }

            return wordList;
        }

        public TextAnalysis CreateTextAnalysis()
        {
            TextAnalysis textAnalysis = new TextAnalysis
            {
                WordsCount = CountWords(),
                MaximumWordLength = MaximumWordLength(),
                MinimumWordLength = MinimumWordLength(),
                AverageWordLength = AverageWordLength(),
                MostCommonLetter = MostCommonLetter(),
                WordDensity = WordDensity()
            };

            return textAnalysis;
        }
    }
}
