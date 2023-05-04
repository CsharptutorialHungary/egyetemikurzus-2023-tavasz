using T4XJYT_LGI301.Core.API;

namespace T4XJYT_LGI301
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Getting the text from the API...\n\n");
            ApiDataProvider apiDataProvider = ApiDataProvider.GetInstance();
            string textFromApi = await apiDataProvider.GetTextFromAPI();
        }
    }
}