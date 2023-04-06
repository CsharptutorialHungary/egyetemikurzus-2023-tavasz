using GoogleBooks.Controller;
using GoogleBooks.Model;


ResultController _resultController = new ResultController();

List<SearchResult> results = new();


    results = _resultController.Search("vuk");
    foreach (SearchResult result in results)
         Console.WriteLine(result.Title);



