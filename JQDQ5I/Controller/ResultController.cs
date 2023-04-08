using GoogleBooks.Communication;
using GoogleBooks.Exception;
using GoogleBooks.Model;
using System.ComponentModel;

namespace GoogleBooks.Controller
{

    public class ResultController
    {
        readonly GoogleCommunication _apiManager = new GoogleCommunication();
        public List<SearchResult> searchResults = new List<SearchResult>();



        public async Task<List<List<string>>> SearchAsync(string searchTerms)
        {
            List<SearchResult> results = new();

            GoogleApiSearchResult googleResult = await _apiManager.GoogleResultByParametersAsync(searchTerms);

            if (googleResult.totalItems == null || googleResult.totalItems == 0)
            {
                throw new SearchException("No results");
            }

            //számunkra megjeleníthető formára konvertáljuk
            foreach (var item in googleResult.items)
            {
                results.Add(new SearchResult(item));
            }

            //string listává alakítjuk, hogy táblázatos formában meg lehessen jeleníteni 
            List<List<string>> stringList = ConvertClassListToStringList(results);

            return stringList;
        }

        List<List<string>> ConvertClassListToStringList<T>(List<T> classList) where T : class
        {
            List<List<string>> stringList = new List<List<string>>
            {
                //A stringList első eleme a fejlécet tartalmazó lista lesz.
                GetPropertyNamesToList(classList[0].GetType().GetProperties())
            };

            foreach (T obj in classList)
            {
                List<string> row = new List<string>();

                foreach (var prop in obj.GetType().GetProperties())
                {
                    if (prop.GetValue(obj, null) != null)
                        row.Add(prop.GetValue(obj, null).ToString());
                    else
                        row.Add("-");
                }
                stringList.Add(row);
            }

            return stringList;
        }

        List<string> GetPropertyNamesToList(System.Reflection.PropertyInfo[] properties)
        {
            List<string> propNames = new List<string>();

            foreach (var prop in properties)
            {
                var displayNameAttribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                    .FirstOrDefault() as DisplayNameAttribute;
                propNames.Add(displayNameAttribute?.DisplayName);
            }
            return propNames;
        }


    }
}