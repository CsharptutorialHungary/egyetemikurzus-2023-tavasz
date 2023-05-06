using System.Linq;
using System.Text.RegularExpressions;
using T4XJYT_LGI301.Core;
using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301
{
    public class TextAnalyser : ITextAnalyser
    {
        private string RawText;
        private List<Word> CleanedUpWords;
        public TextAnalyser(string textToAnalyze)
        {
            RawText = textToAnalyze;
            CleanedUpWords = CreateWordsFromRawText<Word>();
        }

        public int CountWords()
        {
            if (CleanedUpWords == null) {
                return 0;
            }
            int count = CleanedUpWords
                .Count();
            return count;

            // TODO: Implement CountWords function
            //throw new NotImplementedException();
        }

        public int MaximumWordLength()
        {
            if (CleanedUpWords == null)
            {
                return 0;
            }

            string longest_word = CleanedUpWords
                .OrderByDescending(s => s.Length)
                .First()
                .ToString();

            return longest_word.Length;

            // TODO: Implement MaximumWordLength function
            //throw new NotImplementedException();
        }

        public int MinimumWordLength()
        {
            if (CleanedUpWords == null)
            {
                return 0;
            }

            string shortest_word = CleanedUpWords
                .OrderByDescending(s => s.Length)
                .Last()
                .ToString();

            return shortest_word.Length;

            // TODO: Implement MinimumWordLength function
            // throw new NotImplementedException();
        }

        public double AverageWordLength()
        {
            if (CleanedUpWords == null)
            {
                return 0;
            }

            double average_word_length = CleanedUpWords
                .Average(w => w.Length);

            return average_word_length;

            // TODO: Implement AverageWordLength function
            //throw new NotImplementedException();
        }

        public string MostCommonLetter()
        {
            if (CleanedUpWords == null)
            {
                return "";
            }

            var letterCounts = CleanedUpWords
                .GroupBy(letter => letter)
                .Select(group => new { Letter = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count);

            return letterCounts.First().Letter.ToString();

            // TODO: Implement MostCommonLetter function
            //throw new NotImplementedException();
        }

        public double WordDensity()
        {
            if (CleanedUpWords == null)
            {
                return 0;
            }
                   
            int sum_words = CleanedUpWords.Count();
            var number_of_unique_words = CleanedUpWords
                .Distinct().Count();

            double word_density = number_of_unique_words / sum_words;

            return word_density;
            // TODO: Implement WordDensity function
            //throw new NotImplementedException();
        }

        public List<T> CreateWordsFromRawText<T>() where T : Word, new()
        {
            // Don't forget to remove the [" from the start and "] from the end of the text.
            // Also remove every symbol such as .,!? etc.
            // Remove [" from the start and "] from the end of the text
            string trimmedText = RawText.TrimStart('[').TrimEnd(']');

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
