using GoogleBooks.Exception;
using RestSharp;
using System.Net;
namespace GoogleBooks.Communication;

public static class RestCommunication
{

    public static async Task<RestResponse> RestApiCommunicationAsync(Uri uri, Method methodName, Dictionary<string, string> parameters = null)
    {
        RestResponse requestResult;
        var _client = new RestClient();

        var request = new RestRequest(uri) { Method = methodName };

        if (parameters != null)
            parameters.ToList().ForEach(param => request.AddParameter(param.Key, param.Value));

        requestResult = await _client.ExecuteAsync(request);

        if (requestResult.StatusCode != HttpStatusCode.OK)
        {
            throw new SearchException($"Fail in API communication. statuscode: {requestResult.StatusCode}; error: {requestResult.ErrorMessage}", true);
        }

        return requestResult;
    }
}
