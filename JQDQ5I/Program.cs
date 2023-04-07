using GoogleBooks.Controller;
using GoogleBooks.View;


ResultController _resultController = new ResultController();



var results = _resultController.Search("vuk");


PrintTableHelper.PrintTableFromList(results);


