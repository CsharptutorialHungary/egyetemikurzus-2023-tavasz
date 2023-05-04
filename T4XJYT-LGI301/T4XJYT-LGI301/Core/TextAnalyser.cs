using System;
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
            // TODO: Implement CountWords function
            throw new NotImplementedException();
        }

        public int MaximumWordLength()
        {
            // TODO: Implement MaximumWordLength function
            throw new NotImplementedException();
        }

        public int MinimumWordLength()
        {
            // TODO: Implement MinimumWordLength function
            throw new NotImplementedException();
        }

        public double AverageWordLength()
        {
            // TODO: Implement AverageWordLength function
            throw new NotImplementedException();
        }

        public char MostCommonLetter()
        {
            // TODO: Implement MostCommonLetter function
            throw new NotImplementedException();
        }

        public double WordDensity()
        {
            // TODO: Implement WordDensity function
            throw new NotImplementedException();
        }

        public List<Word> CreateWordsFromRawText()
        {
            // TODO: Implement CreateWordsFromRawText function
            // Don't forget to remove the [" from the start and "] from the end of the text.
            throw new NotImplementedException();
        }

        public TextAnalysis CreateTextAnalysis()
        {
            // TODO: Implement CreateTextAnalysis
            throw new NotImplementedException();
        }
    }

}

