using T4XJYT_LGI301.Core.API;
using T4XJYT_LGI301.Core.IO;
using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Getting the text from the API...\n\n");
            ApiDataProvider apiDataProvider = new ApiDataProvider();
            string textFromApi = await apiDataProvider.GetTextFromAPI();
            Console.WriteLine(textFromApi);
            Console.WriteLine("Testing the saving XML method");
            
            AnalysisFileSaver textAnalysisService = new AnalysisFileSaver();

            TextAnalysis textAnalysis = new TextAnalysis
            {
                WordsCount = 100,
                MaximumWordLength = 15,
                MinimumWordLength = 1,
                AverageWordLength = 5,
                MostCommonLetter = 'e',
                WordDensity = 0.1
            };

            await textAnalysisService.SaveAnalysis(textAnalysis, FileFormat.Json);
        }
    }
}