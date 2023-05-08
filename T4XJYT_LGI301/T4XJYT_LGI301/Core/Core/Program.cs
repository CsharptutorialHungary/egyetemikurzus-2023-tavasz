using System;
using System.Linq;
using System.Threading.Tasks;
using T4XJYT_LGI301.Core.API;
using T4XJYT_LGI301.Core.IO;
using T4XJYT_LGI301.Core.Models;

namespace T4XJYT_LGI301.Core.Core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Text Analysis Project!");

            bool runApp = true;
            while (runApp)
            {
                Console.WriteLine("1. Download a new text from the API");
                Console.WriteLine("2. Exit");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        await DownloadAndSaveText();
                        break;
                    case "2":
                        runApp = false;
                        Console.WriteLine("Bye! :]");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static async Task DownloadAndSaveText()
        {
            string textFromApi = await FetchTextFromApi();
            bool shouldSave = PromptToSaveText();

            if (shouldSave)
            {
                FileFormat? fileFormat = DetermineFileFormat();
                if (fileFormat != null)
                {
                    await SaveText(textFromApi, (FileFormat)fileFormat);
                }
            }
        }

        static async Task<string> FetchTextFromApi()
        {
            Console.WriteLine("Getting the text from the API...\n");
            ApiDataProvider apiDataProvider = new ApiDataProvider();
            string textFromApi = await apiDataProvider.GetTextFromAPI();
            Console.WriteLine(textFromApi);
            return textFromApi;
        }

        static bool PromptToSaveText()
        {
            Console.WriteLine("\nDo you want to save the text to a file? (yes/no)");
            string saveToFile = Console.ReadLine().ToLower();
            string[] yesAnswers = new string[4] {"y", "yes", "igen", "i"};

            return yesAnswers.Contains(saveToFile);
        }

        static FileFormat? DetermineFileFormat()
        {
            Console.WriteLine("Select the file format:");
            Console.WriteLine("1. XML");
            Console.WriteLine("2. JSON");
            string formatOption = Console.ReadLine();

            switch (formatOption)
            {
                case "1":
                    return FileFormat.Xml;
                case "2":
                    return FileFormat.Json;
                default:
                    Console.WriteLine("Invalid option. Skipping save operation.");
                    return null;
            }
        }

        static async Task SaveText(string textFromApi, FileFormat fileFormat)
        {
            AnalysisFileSaver textAnalysisService = new AnalysisFileSaver();
            TextAnalyser textAnalyser = new TextAnalyser(textFromApi);
            TextAnalysis textAnalysis = textAnalyser.CreateTextAnalysis();

            await textAnalysisService.SaveAnalysis(textAnalysis, fileFormat);
            Console.WriteLine($"Text analysis saved in {fileFormat} format.");
        }
    }
}
