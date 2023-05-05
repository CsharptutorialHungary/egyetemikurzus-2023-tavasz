namespace T4XJYT_LGI301.Core.API
{
    public class ApiDataProvider : IApiDataProvider
    {
        private static ApiDataProvider _instance;

        private readonly string _apiURL = "https://baconipsum.com/api/?type=meat-and-filler";

        private ApiDataProvider() {}

        public static ApiDataProvider GetInstance()
        {
            if (_instance == null)
                _instance = new ApiDataProvider();

            return _instance;
        }

        public async Task<string> GetTextFromAPI()
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(_apiURL);

                if (response.IsSuccessStatusCode)
                {
                    string text = await response.Content.ReadAsStringAsync();
                    return text;
                }
                else
                {
                    // Handle unsuccessful status codes
                    string errorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    throw new HttpRequestException(errorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HttpRequestException - this can occur due to unsuccessful status codes or other request-related issues
                Console.WriteLine($"Request error: {ex.Message}");
                return string.Empty;
            }
            catch (TaskCanceledException ex)
            {
                // Handle TaskCanceledException - this can occur due to a request timeout or cancellation
                Console.WriteLine($"Request timeout or canceled: {ex.Message}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                // Handle any other unhandled exceptions
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return string.Empty;
            }
        }
    }
}

