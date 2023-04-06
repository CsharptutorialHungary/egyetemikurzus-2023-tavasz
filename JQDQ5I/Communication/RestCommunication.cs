using RestSharp;
using System.Net;
namespace GoogleBooks.Communication;

public static class RestCommunication
{

    public static RestResponse RestApiCommunication(Uri uri, Method methodName, Dictionary<string, string> parameters = null)
    {
        RestResponse requestResult;
        var _client = new RestClient();

        var request = new RestRequest(uri) { Method = methodName };

        if (parameters != null)
            foreach (KeyValuePair<string, string> param in parameters)
            {
                request.AddParameter(param.Key, param.Value);
            }

        requestResult = _client.Execute(request);

        if (requestResult.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception($"Hiba az API kommunikációban. Státuszkód: {requestResult.StatusCode}; hiba: {requestResult.ErrorMessage}");
        }

        return requestResult;
    }
}