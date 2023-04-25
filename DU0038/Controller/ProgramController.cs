using DU0038.Model;
using DU0038.Service;

namespace DU0038.Controller;

public class ProgramController
{

    private string? _command = "";

    public void StartProgramLoop()
    {
        Console.WriteLine("\n\t##### Üdvözöllek az alkalmazásban #####");
        Console.WriteLine("A megadható parancsok listázásához add meg a 'help' parancsot!");
        Console.WriteLine("Ha az alkalmazásból nem az 'exit' parancsal lépsz ki, az adatok el fognak veszni!");
        while (true)
        {
            Console.Write("\n# Adjon meg egy parancsot: ");
            _command = Console.ReadLine();
            if (!IsCommandInvalid()) EvaluateCommand();
            else Console.WriteLine("# Nem adtál meg parancsot!");
        }
    }

    private bool IsCommandInvalid()
    {
        return _command == null || _command.Trim() == "";
    }

    private void EvaluateCommand()
    {
        switch (_command)
        {
            case Commands.AddCategory:
                AddCategory();
                break;
            case Commands.Exit:
                SaveStateAndExitProgram();
                break;
            case Commands.Help:
                ListCommands();
                break;
            case Commands.ListCategory:
                ListCategory();
                break;
            case Commands.AddTransaction:
                AddTransaction();
                break;
            case Commands.ListIncomes:
                ListIncomes();
                break;
            case Commands.ListExpenses:
                ListExpenses();
                break;
            case Commands.GetBalance:
                GetBalance();
                break;
            case Commands.GetSumIncome:
                GetSumIncome();
                break;
            case Commands.GetSumExpense:
                GetSumExpense();
                break;
            case Commands.ListExpensesByCategory:
                ListExpensesByCategory();
                break;
            case Commands.ListIncomesByCategory:
                ListIncomesByCategory();
                break;
            default:
                Console.WriteLine("Nem létezik ilyen parancs! Parancsok kilistázásához használd a 'help'-et!");
                break;
        }
    }

    private void AddCategory()
    {
        string? categoryName = null;
        string? isIncomeString = null;

        _command = null;
        while (IsCommandInvalid())
        {
            Console.Write("## Adjon meg egy nevet a kategóriának: ");
            _command = Console.ReadLine();
            if (IsCommandInvalid()) Console.WriteLine("## Érvénytelen érték!");
            else categoryName = _command;
        }

        _command = null;
        while (IsCommandInvalid() || (_command != "+" && _command != "-"))
        {
            Console.Write("## Bevétel (+) vagy költség (-) kategória lesz: ");
            _command = Console.ReadLine();
            if (IsCommandInvalid() || (_command != "+" && _command != "-"))
                Console.WriteLine("## Érvénytelen érték! Adjon meg + vagy - karaktert!");
            else isIncomeString = _command;
        }
        CategoryService.Instance.AddCategory(categoryName, isIncomeString == "+");
        Console.WriteLine("### Kategória hozzáadása sikeres!");
    }

    private void SaveStateAndExitProgram()
    {
        CategoryService.Instance.SaveCategories();
        TransactionService.Instance.SaveTransactions();
        Environment.Exit(0);
    }

    private void ListCommands()
    {
        Console.WriteLine("## Megadható parancsok listája:");
        var type = typeof(Commands);
        foreach (var p in type.GetFields( System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
        {
            var v = p.GetValue(null);
            Console.WriteLine("\t - " + v);
        }
    }

    private void ListCategory()
    {
        Console.WriteLine("## Kategóriák listája:");
        var categoryNames = CategoryService.Instance.GetCategories().Select(category => category.Name)
            .OrderBy(name => name);

        var i = 1;
        foreach (var name in categoryNames)
        {
            Console.WriteLine("\t " + i++ + ". " + name);
        }
    }

    private void AddTransaction()
    {
        string? transactionName = null;
        var categoryId = "";
        var value = 0;

        _command = null;
        while (IsCommandInvalid())
        {
            Console.Write("## Adjon meg egy nevet a tranzakciónak: ");
            _command = Console.ReadLine();
            if (IsCommandInvalid()) Console.WriteLine("## Érvénytelen érték!");
            else transactionName = _command;
        }
        
        Console.WriteLine("## Add meg a választott kategória sorszámát!");
        ListCategory();
        var numberOfCategories = CategoryService.Instance.GetCategories().Count;
        var isParsable = false;
        var categoryIndex = 0;
        _command = null;
        while (IsCommandInvalid() || !isParsable || categoryIndex < 1 || categoryIndex > numberOfCategories)
        {
            Console.Write("## Választott kategória sorszáma: ");
            _command = Console.ReadLine();
            isParsable = int.TryParse(_command, out categoryIndex);
            if (IsCommandInvalid() || !isParsable || categoryIndex < 1 || categoryIndex > numberOfCategories)
                Console.WriteLine("## Érvénytelen érték!");
            else
            {
                var categories = CategoryService.Instance.GetCategories()
                    .OrderBy(category => category.Name);
                categoryId = categories.ElementAt(categoryIndex - 1).Id;
            }
        }
        
        Console.WriteLine("## Add meg a tranzakció értékét (pozitív egész szám):");
        _command = null;
        isParsable = false;
        while (IsCommandInvalid() || !isParsable || value <= 0)
        {
            Console.Write("## Érték megadása: ");
            _command = Console.ReadLine();
            isParsable = int.TryParse(_command, out value);
            if (IsCommandInvalid() || !isParsable || value <= 0) Console.WriteLine("Érvénytelen érték!");
            TransactionService.Instance.AddTransaction(transactionName, value, DateTime.Today, categoryId);
        }
        Console.WriteLine("### Tranzakció hozzáadása sikeres!");
    }

    private void ListIncomes()
    {
        Console.WriteLine("## Bevételek listája:");
        foreach (var transaction in TransactionService.Instance.GetIncomeTransactions())
        {
            Console.WriteLine($"\t {transaction.Date:yyyy. MM. dd.} {transaction.Name} +{transaction.Value} Ft");
        }
    }
    
    private void ListExpenses()
    {
        Console.WriteLine("## Kiadások listája:");
        foreach (var transaction in TransactionService.Instance.GetExpenseTransactions())
        {
            Console.WriteLine($"\t {transaction.Date:yyyy. MM. dd.} {transaction.Name} -{transaction.Value} Ft");
        }
    }

    private void GetBalance()
    {
        Console.WriteLine($"## Egyenleg: {TransactionService.Instance.GetBalance()} Ft");
    }
    
    private void GetSumExpense()
    {
        Console.WriteLine($"## Összesített kiadás: {TransactionService.Instance.GetSumExpense()} Ft");
    }
    
    private void GetSumIncome()
    {
        Console.WriteLine($"## Összesített bevétel: {TransactionService.Instance.GetSumIncome()} Ft");
    }

    private void ListExpensesByCategory()
    {
        Console.WriteLine("## Költségek kategóriánként:");
        foreach (var category in CategoryService.Instance.GetCategories().FindAll(category => !category.IsIncome))
        {
            Console.WriteLine($"## {category.Name}:");
            foreach (var transaction in TransactionService.Instance.GetExpensesByCategory(category.Id))
            {
                Console.WriteLine($"\t {transaction.Date:yyyy. MM. dd.} {transaction.Name} -{transaction.Value} Ft");
            }
        }
    }
    
    private void ListIncomesByCategory()
    {
        Console.WriteLine("## Bevételek kategóriánként:");
        foreach (var category in CategoryService.Instance.GetCategories().FindAll(category => category.IsIncome))
        {
            Console.WriteLine($"## {category.Name}:");
            foreach (var transaction in TransactionService.Instance.GetIncomesByCategory(category.Id))
            {
                Console.WriteLine($"\t {transaction.Date:yyyy. MM. dd.} {transaction.Name} +{transaction.Value} Ft");
            }
        }
    }
}