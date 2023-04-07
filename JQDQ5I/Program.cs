using GoogleBooks.Controller;
using GoogleBooks.Model;
using GoogleBooks.View;


ResultController _resultController = new ResultController();


List<SearchResult> results = new();

results = _resultController.Search("vuk");


PrintTableHelper.PrintTableFromList(results);


