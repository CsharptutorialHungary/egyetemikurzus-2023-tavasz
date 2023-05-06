using T4XJYT_LGI301.Core.Models;
using Xunit;

namespace T4XJYT_LGI301.Tests
{
    public class TextAnalyserTests
    {
        
        [Theory]
        [InlineData("this is a sample text it contains words of different lengths", 11)]
        [InlineData("", 0)]
        [InlineData("hello world", 2)]
        [InlineData("count words in this sentence", 5)]
        public void TextAnalyser_CountWords_ReturnsInt(string textInput, int expectedCount)
        {
            // Arrange
            TextAnalyser sut = new TextAnalyser(textInput);
    
            // Act
            int result = sut.CountWords();
    
            // Assert
            Assert.Equal(expectedCount, result);
        }

        [Theory]
        [InlineData("This is a sample text. It contains words of different lengths.", 9)]
        [InlineData("", 0)]
        [InlineData("Hello world!", 5)]
        [InlineData("Count words in this sentence.", 8)]
        public void TextAnalyser_CountMaximumWordLength_ReturnsInt(string textInput, int expectedMaxWordLength)
        {
            // Arrange
            TextAnalyser sut = new TextAnalyser(textInput);
    
            // Act
            int result = sut.MaximumWordLength();
    
            // Assert
            Assert.Equal(expectedMaxWordLength, result);
        }

        [Theory]
        [InlineData("This is a sample text. It contains words of different lengths.", 1)]
        [InlineData("", 0)]
        [InlineData("Hello world!", 5)]
        [InlineData("Count words in this sentence.", 2)]
        public void TextAnalyser_CountMinimumWordLength_ReturnsInt(string textInput, int expectedMinWordLength)
        {
            // Arrange
            TextAnalyser sut = new TextAnalyser(textInput);
    
            // Act
            int result = sut.MinimumWordLength();
    
            // Assert
            Assert.Equal(expectedMinWordLength, result);
        }

        [Theory]
        [InlineData("This is a sample text. It contains words of different lengths.", 4.54)]
        [InlineData("", 0)]
        [InlineData("Hello world!", 5)]
        [InlineData("Count words in this sentence.", 4.8)]
        public void TextAnalyser_AverageWordLength_ReturnsInt(string textInput, double expectedAvgWordLength)
        {
            // Arrange
            TextAnalyser sut = new TextAnalyser(textInput);
    
            // Act
            double result = sut.AverageWordLength();
    
            // Assert
            Assert.Equal(expectedAvgWordLength, result, 2);
        }
        
        [Theory]
        [InlineData("This is a sample text. It contains words of different lengths.", "t")]
        [InlineData("", "NA")]
        [InlineData("Hello world!", "l")]
        [InlineData("Count words in this sentence.", "t")]
        public void TextAnalyser_MostCommonLetter_ReturnsString(string textInput, string expectedMostCommonLetter)
        {
            // Arrange
            TextAnalyser sut = new TextAnalyser(textInput);
    
            // Act
            string result = sut.MostCommonLetter();
    
            // Assert
            Assert.Equal(expectedMostCommonLetter, result);
        }
        
        [Theory]
        [InlineData("This is a sample text. It contains words of different lengths.", 1)]
        [InlineData("", 0)]
        [InlineData("$#@,.", 0)]
        [InlineData("Hello world!", 1)]
        [InlineData("Count words in this sentence. This sentence is filled with words.", 0.72)]
        public void TextAnalyser_WordDensity_ReturnsString(string textInput, double expectedWordDensity)
        {
            // Arrange
            TextAnalyser sut = new TextAnalyser(textInput);
    
            // Act
            double result = sut.WordDensity();
    
            // Assert
            Assert.Equal(expectedWordDensity, result, 2);
        }
        
        [Theory]
        [InlineData("[\"This is a sample text. It contains words of different lengths.\"]", new[] { "this", "is", "a", "sample", "text", "it", "contains", "words", "of", "different", "lengths" })]
        [InlineData("[\"Hello, World!\"]", new[] { "hello", "world" })]
        [InlineData("[\"\"]", new string[] { })]
        [InlineData("[\"one\"]", new[] { "one" })]
        [InlineData("[\"%$#\"]", new string[] {  })]
        [InlineData("[\"125654\"]", new string[] {  })]
        [InlineData("[\"One, two1, three.\"]", new[] { "one", "two", "three" })]
        [InlineData("[\"two, two, three.\"]", new[] { "two", "two", "three" })]
        public void TextAnalyser_CreateWordsFromRawText_ReturnsGenericList(string input, string [] expectedWords)
        {
            // Arrange
            TextAnalyser sut = new TextAnalyser(input);

            // Act
            var results = sut.CreateWordsFromRawText<Word>().ToArray();

            // Assert
            Assert.Equal(expectedWords.Length, results.Length);
            for (int i = 0; i < expectedWords.Length; i++)
            {
                Assert.Equal(expectedWords[i], results[i].Text);
            }
        }
        
        [Theory]
        [InlineData("This is a sample text. It contains words of different lengths.", 11, 9, 1, 4, "t", 1)]
        [InlineData("", 0, 0, 0, 0, "NA", 0)]
        [InlineData("Hello world!", 2, 5, 5, 5, "l", 1)]
        [InlineData("Count words in this sentence.", 5, 8, 2, 4.8, "t", 1)]
        public void CreateTextAnalysis_ReturnsCorrectResult(string input, int expectedWordsCount, int expectedMaxWordLength, int expectedMinWordLength, int expectedAvgWordLength, string expectedMostCommonLetter, double expectedWordDensity)
        {
            // Arrange
            var expectedAnalysis = new TextAnalysis
            {
                WordsCount = expectedWordsCount,
                MaximumWordLength = expectedMaxWordLength,
                MinimumWordLength = expectedMinWordLength,
                AverageWordLength = expectedAvgWordLength,
                MostCommonLetter = expectedMostCommonLetter,
                WordDensity = expectedWordDensity
            };
            var sut = new TextAnalyser(input);

            // Act
            var result = sut.CreateTextAnalysis();

            // Assert
            Assert.Equal(expectedAnalysis.WordsCount, result.WordsCount);
            Assert.Equal(expectedAnalysis.MaximumWordLength, result.MaximumWordLength);
            Assert.Equal(expectedAnalysis.MinimumWordLength, result.MinimumWordLength);
            Assert.Equal(expectedAnalysis.AverageWordLength, result.AverageWordLength);
            Assert.Equal(expectedAnalysis.MostCommonLetter, result.MostCommonLetter);
            Assert.InRange(result.WordDensity, expectedAnalysis.WordDensity - 0.001, expectedAnalysis.WordDensity + 0.001);
        }
    }
}
