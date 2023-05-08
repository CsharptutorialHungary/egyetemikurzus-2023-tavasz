using GoogleBooks.Controller;
using GoogleBooks.Exception;
using GoogleBooks.ViewCommon;
using System.Text;

namespace GoogleBooks.Command;
public class SearchCommand : ICommand
{
    public string Name => "search";

    public async Task ExecuteAsync(string[] args)
    {
        if (args.Length < 1)
        {
            throw new SearchException("Please provide at least one parameter.");
        }
        try
        {
            StringBuilder parameters = new StringBuilder(70);

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-t":
                    case "--title":
                        i++;
                        parameters.Append("+intitle:");
                        parameters.Append(args[i]);
                        break;
                    case "-i":
                    case "--isbn":
                        i++;
                        var isbn = args[i];
                        if (!isbn.All(char.IsDigit) || isbn.Length != 13)
                            throw new SearchException($"ISBN is a numeric commercial book identifier from 13 digit.");

                        parameters.Append("+isbn:");
                        parameters.Append(isbn);
                        break;
                    case "-a":
                    case "--author":
                        i++;
                        parameters.Append("+inauthor:");
                        parameters.Append(args[i]);
                        break;
                    default:
                        throw new SearchException($"Unknown argument: {args[i]}");
                }
            }


            ResultController _resultController = new ResultController();

            List<List<string>> results = await Task.Run(() => _resultController.SearchAsync(parameters.ToString()));

            PrintTableHelper.PrintTableFromList(results);
        }
        catch (SearchException e)
        {
            throw e;
        }        
        catch (IndexOutOfRangeException e)
        {
            throw new SearchException("Missing search term!");
        }

    }
}

