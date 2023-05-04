using System;
using T4XJYT_LGI301.Core;
using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301
{
    public class TextAnalyser : ITextAnalyser
    {
        public string TextToAnalyze { get; set; }

        public Word[] Words { get; set; }

        public TextAnalyser(string textToAnalyze) => TextToAnalyze = textToAnalyze;

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
    }

}

