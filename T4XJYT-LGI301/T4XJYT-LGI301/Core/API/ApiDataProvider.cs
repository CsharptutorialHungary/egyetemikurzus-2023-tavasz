using System;
using static System.Net.WebRequestMethods;

namespace T4XJYT_LGI301.Core.API
{
    public class ApiDataProvider : IApiDataProvider
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiURL = "https://baconipsum.com/api/?type=meat-and-filler";

        public ApiDataProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<string> GetTextFromAPI()
        {
            // TODO:
            throw new NotImplementedException();
        }
    }
}

