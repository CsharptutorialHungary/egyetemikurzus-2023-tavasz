using DU0038.Model;
using DU0038.Service;

namespace DU0038.Controller;

public class ProgramController
{

    private string? _command = "";

    public ProgramController()
    {
        InitializeProgram();
    }

    private void InitializeProgram()
    {
        Task.WaitAll(CategoryService.Instance.InitializeCategories(), TransactionService.Instance.InitializeTransactions());
        StartProgramLoop();
    }

    private void StartProgramLoop()
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
            if (_command == Commands.Exit) return;
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
                HandleAddCategory();
                break;
            case Commands.Exit:
                HandleSaveStateAndExitProgram();
                break;
            case Commands.Help:
                HandleListCommands();
                break;
            case Commands.ListCategory:
                HandleListCategory();
                break;
            case Commands.AddTransaction:
                HandleAddTransaction();
                break;
            case Commands.ListIncomes:
                HandleListIncomes();
                break;
            case Commands.ListExpenses:
                HandleListExpenses();
                break;
            case Commands.GetBalance:
                HandleGetBalance();
                break;
            case Commands.GetSumIncome:
                HandleGetSumIncome();
                break;
            case Commands.GetSumExpense:
                HandleGetSumExpense();
                break;
            case Commands.ListExpensesByCategory:
                HandleListExpensesByCategory();
                break;
            case Commands.ListIncomesByCategory:
                HandleListIncomesByCategory();
                break;
            default:
                Console.WriteLine("# Nem létezik ilyen parancs! Parancsok kilistázásához használd a 'help'-et!");
                break;
        }
    }

    private void HandleAddCategory()
    {
        var categoryName = GetNameForNewCategory();
        var isIncome = GetIsIncomeForNewCategory();
        
        CategoryService.Instance.AddCategory(categoryName, isIncome);
        Console.WriteLine("### Kategória hozzáadása sikeres!");
    }

    private string GetNameForNewCategory()
    {
        _command = null;
        while (IsCommandInvalid())
        {
            Console.Write("## Adjon meg egy nevet a kategóriának: ");
            _command = Console.ReadLine();
            if (IsCommandInvalid()) Console.WriteLine("## Érvénytelen érték!");
            else break;
        }

        return _command!;
    }

    private bool GetIsIncomeForNewCategory()
    {
        _command = null;
        while (IsCommandInvalid() || (_command != "+" && _command != "-"))
        {
            Console.Write("## Bevétel (+) vagy költség (-) kategória lesz: ");
            _command = Console.ReadLine();
            if (IsCommandInvalid() || (_command != "+" && _command != "-"))
                Console.WriteLine("## Érvénytelen érték! Adjon meg + vagy - karaktert!");
            else break;
        }

        return _command! == "+";
    }

    private void HandleSaveStateAndExitProgram()
    {
        Task.WaitAll(TransactionService.Instance.SaveTransactions(),
            CategoryService.Instance.SaveCategories());
        Environment.Exit(0);
    }

    private void HandleListCommands()
    {
        Console.WriteLine("## Megadható parancsok listája:");
        var type = typeof(Commands);
        foreach (var p in type
                     .GetFields( System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
        {
            var v = p.GetValue(null);
            Console.WriteLine("\t - " + v);
        }
    }

    private void HandleListCategory()
    {
        Console.WriteLine("## Kategóriák listája:");
        var categoriesInNameOrder = CategoryService.Instance.GetCategoriesInNameOrder();

        var i = 1;
        foreach (var category in categoriesInNameOrder)
        {
            Console.WriteLine($"\t {i++}. {category.Name} ({(category.IsIncome ? "+" : "-")})");
        }
    }

    private void HandleAddTransaction()
    {
        if (CategoryService.Instance.GetCategories().Count == 0)
        {
            Console.WriteLine("## Nincs még kategóriád! " +
                              "Tranzakció létrehozása előtt hozz létre kategóriát az 'add category' paranccsal!");
            return;
        }
        
        string transactionName = GetNameForNewTransaction();
        var categoryId = GetCategoryForNewTransaction();
        var value = GetValueForNewTransaction();
        
        TransactionService.Instance.AddTransaction(transactionName, value, DateTime.Today, categoryId);
        Console.WriteLine("### Tranzakció hozzáadása sikeres!");
    }

    private string GetNameForNewTransaction()
    {
        _command = null;
        while (IsCommandInvalid())
        {
            Console.Write("## Adjon meg egy nevet a tranzakciónak: ");
            _command = Console.ReadLine();
            if (IsCommandInvalid()) Console.WriteLine("## Érvénytelen érték!");
            else break;
        }
        return _command!;
    }

    private string GetCategoryForNewTransaction()
    {
        Console.WriteLine("## Add meg a választott kategória sorszámát!");
        HandleListCategory();
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
            else break;
        }

        var categories = CategoryService.Instance.GetCategories()
            .OrderBy(category => category.Name);
        return categories.ElementAt(categoryIndex - 1).Id;
    }

    private int GetValueForNewTransaction()
    {
        Console.WriteLine("## Add meg a tranzakció értékét (pozitív egész szám):");
        var isParsable = false;
        var value = 0;
        _command = null;
        while (IsCommandInvalid() || !isParsable || value <= 0)
        {
            Console.Write("## Érték megadása: ");
            _command = Console.ReadLine();
            isParsable = int.TryParse(_command, out value);
            if (IsCommandInvalid() || !isParsable || value <= 0) Console.WriteLine("## Érvénytelen érték!");
            else break;
        }

        return value;
    }

    private void HandleListIncomes()
    {
        Console.WriteLine("## Bevételek listája:");
        foreach (var transaction in TransactionService.Instance.GetIncomeTransactions())
        {
            Console.WriteLine($"\t {transaction.Date:yyyy. MM. dd.} {transaction.Name} +{transaction.Value} Ft");
        }
    }
    
    private void HandleListExpenses()
    {
        Console.WriteLine("## Kiadások listája:");
        foreach (var transaction in TransactionService.Instance.GetExpenseTransactions())
        {
            Console.WriteLine($"\t {transaction.Date:yyyy. MM. dd.} {transaction.Name} -{transaction.Value} Ft");
        }
    }

    private void HandleGetBalance()
    {
        Console.WriteLine($"## Egyenleg: {TransactionService.Instance.GetBalance()} Ft");
    }
    
    private void HandleGetSumExpense()
    {
        Console.WriteLine($"## Összesített kiadás: {TransactionService.Instance.GetSumExpense()} Ft");
    }
    
    private void HandleGetSumIncome()
    {
        Console.WriteLine($"## Összesített bevétel: {TransactionService.Instance.GetSumIncome()} Ft");
    }

    private void HandleListExpensesByCategory()
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
    
    private void HandleListIncomesByCategory()
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