using GoogleBooks.Controller;
using GoogleBooks.View;

namespace GoogleBooks.Command;
public class SearchCommand : ICommand
{
    public string Name => "search";

    public void Execute(string term)
    {
        if (term.Length < 1)
        {
            throw new Exception("Please provide a search term.");
        }

        ResultController _resultController = new ResultController();

        List<List<string>> results = new();
        try
        {
            results = _resultController.Search(term);
        }
        catch (Exception e)
        {
            throw e;
        }

        PrintTableHelper.PrintTableFromList(results);
    }
}

