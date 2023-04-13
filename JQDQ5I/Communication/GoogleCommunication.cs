using Newtonsoft.Json;
using GoogleBooks.Model;
using System.Configuration;
namespace GoogleBooks.Communication;

public class GoogleCommunication
{
    private readonly string baseUrl;

    public GoogleCommunication()
    {
        baseUrl = ConfigurationManager.AppSettings["Google.Api.BaseUrl"];
    }

    public async Task<GoogleApiSearchResult> GoogleResultByParametersAsync(string parameter)
    {
        var uri = new Uri(baseUrl + "volumes/?q=" + parameter);

        var response = await RestCommunication.RestApiCommunicationAsync(uri, RestSharp.Method.Get);

        var results = JsonConvert.DeserializeObject<GoogleApiSearchResult>(response.Content);

        return results;
    }
}