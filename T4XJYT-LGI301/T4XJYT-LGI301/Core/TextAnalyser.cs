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

            Word longestWord = _cleanedUpWords.OrderByDescending(w => w.Length).FirstOrDefault();
            return longestWord.Length;

            //string longestWord = _cleanedUpWords
            //    .OrderByDescending(s => s.Length)
            //    .First()
            //    .ToString();

            //return longestWord.Length;
        }

        public int MinimumWordLength()
        {
            if (!_cleanedUpWords.Any()) return 0;

            string shortestWord = _cleanedUpWords
                .OrderByDescending(s => s.Length)
                .Last()
                .ToString();

            return shortestWord.Length;
        }

        public double AverageWordLength()
        {
            if (!_cleanedUpWords.Any()) return 0;

            double averageWordLength = _cleanedUpWords
                .Average(w => w.Length);

            return averageWordLength;
        }

        public string MostCommonLetter()
        {
            if (!_cleanedUpWords.Any()) return "NA";

            var letterCounts = _cleanedUpWords
                .GroupBy(letter => letter)
                .Select(group => new { Letter = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count);

            return letterCounts.First().Letter.ToString();
        }

        public double WordDensity()
        {
            if (!_cleanedUpWords.Any()) return 0;

            int sumWords = _cleanedUpWords.Count();
            var numberOfUniqueWords = _cleanedUpWords
                .Distinct().Count();

            double wordDensity = numberOfUniqueWords / sumWords;

            return wordDensity;
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
                T newWord = new T { Text = word.ToLowerInvariant()};
                wordList.Add(newWord);
            }

            return wordList;
        }

        public TextAnalysis CreateTextAnalysis()
        {
            // TODO: Implement CreateTextAnalysis
            throw new NotImplementedException();
        }
    }

}
