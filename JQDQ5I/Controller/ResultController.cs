using GoogleBooks.Communication;
using GoogleBooks.Model;
using System.Text;

namespace GoogleBooks.Controller
{

    public class ResultController
    {
        readonly GoogleCommunication _apiManager = new GoogleCommunication();
        public List<SearchResult> searchResults = new List<SearchResult>();


        public List<SearchResult> Search(string keyword = "", string isbn = "", string author = "")
        {
            var results = new List<SearchResult>();

            StringBuilder parameters = new StringBuilder(70);
            if (keyword != "")
            {
                parameters.Append("+intitle:");
                parameters.Append(keyword);
            }
            if (isbn != "")
            {
                parameters.Append("+isbn:");
                parameters.Append(isbn);
            }
            if (author != "")
            {
                parameters.Append("+inauthor:");
                parameters.Append(author);
            }
            if (parameters.Length == 0) throw new Exception("Nincs megadva keresési feltétel!");

            GoogleApiSearchResult googleResult = _apiManager.GoogleResultByParameters(parameters.ToString());


            if (googleResult.totalItems == null || googleResult.totalItems == 0)
            {
                throw new Exception("Nincs találat!");
            }
            foreach (var item in googleResult.items)
            {
                results.Add(new SearchResult(item));
            }

            return results;
        }

    }
}