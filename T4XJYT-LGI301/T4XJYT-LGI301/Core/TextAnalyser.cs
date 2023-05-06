using System.Linq;
using T4XJYT_LGI301.Core;
using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301
{
    public class TextAnalyser : ITextAnalyser
    {
        public string RawText { get; set; }

        public TextAnalyser(string textToAnalyze)
        {
            RawText = textToAnalyze;
        }

        public int CountWords()
        {
            //Count words in text basic

            /*
             * char[] delimiters = new char[] { ' ', '\r', '\n' };
             * RawText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
             */

            char[] delimiters = new char[] { ' ', '\r', '\n', '.', '?', '!' };
            String[] raw_text_split = RawText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            int count = raw_text_split.Count();
            return count;

            // TODO: Implement CountWords function
            //throw new NotImplementedException();
        }

        public int MaximumWordLength()
        {
            //Max word length

            //char[] delimiters = new char[] { ' ', '\r', '\n' };
            //int longest_word_length = -1;
            //String longest_word = "";

            //var words = RawText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            //for(String word in words) {
            //    if (word.Length > longest_word) { 
            //        longest_word = word;
            //        longest_word_length = word.Length;
            //    }
            //}

            char[] delimiters = new char[] { ' ', '\r', '\n', '.', '?', '!' };
            String[] raw_text_split = RawText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            string longest_word = raw_text_split.OrderByDescending(s => s.Length).First();

            return longest_word.Length;

            // TODO: Implement MaximumWordLength function
            //throw new NotImplementedException();
        }

        public int MinimumWordLength()
        {

            //char[] delimiters = new char[] { ' ', '\r', '\n' };
            //int longest_word_length = int.MaxValue;
            //String longest_word = "";

            //var words = RawText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            //for(String word in words)
            //{
            //    if (word.Length < longest_word)
            //    {
            //        longest_word = word;
            //        longest_word_length = word.Length;
            //    }
            //}

            char[] delimiters = new char[] { ' ', '\r', '\n', '.', '?', '!' };
            String[] raw_text_split = RawText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            string longest_word = raw_text_split.OrderByDescending(s => s.Length).Last();

            return longest_word.Length;

            // TODO: Implement MinimumWordLength function
            // throw new NotImplementedException();
        }

        public double AverageWordLength()
        {

            char[] delimiters = new char[] { ' ', '\r', '\n', '.', '?', '!' };
            String[] raw_text_split = RawText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            double average_word_length = raw_text_split.Average(w => w.Length);

            return average_word_length;


            // TODO: Implement AverageWordLength function
            //throw new NotImplementedException();
        }

        public string MostCommonLetter()
        {
            var charMap = RawText.Distinct().ToDictionary(c => c, c => RawText.Count(s => s == c));
            return charMap.OrderByDescending(kvp => kvp.Value)
                .First().Key.ToString();

            // TODO: Implement MostCommonLetter function
            //throw new NotImplementedException();
        }

        public double WordDensity()
        {
            char[] delimiters = new char[] { ' ', '\r', '\n', '.', '?', '!' };
            String[] raw_text_split = RawText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            double word_density = 0;
            var sum_words = raw_text_split.Count();

            var number_of_unique_words = raw_text_split
              .GroupBy(s => s)
              .Where(g => g.Count() == 1)
              .Select(g=>g.Key).Count();

            word_density = number_of_unique_words / sum_words;

            return word_density;
            // TODO: Implement WordDensity function
            //throw new NotImplementedException();
        }

        public List<T> CreateWordsFromRawText<T>()
        {
            // TODO: Implement CreateWordsFromRawText function
            // Don't forget to remove the [" from the start and "] from the end of the text.
            // Also remove every symbol such as .,!? etc.
            // This function should not be case sensitive, so "Hello" and "hello" should be the same.
            throw new NotImplementedException();
        }

        public TextAnalysis CreateTextAnalysis()
        {
            // TODO: Implement CreateTextAnalysis
            throw new NotImplementedException();
        }
    }

}
