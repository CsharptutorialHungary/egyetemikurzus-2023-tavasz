using DU0038.Model;
using DU0038.Service;

namespace DU0038.Controller;

public class ProgramController
{
    public ProgramController()
    {
    }

    public void StartProgramLoop()
    {
        Console.WriteLine("\n\t##### Üdvözöllek az alkalmazásban #####");
        Console.WriteLine("A megadható parancsok listázásához add meg a 'help' parancsot!");
        Console.WriteLine("Ha az alkalmazásból nem az 'exit' parancsal lépsz ki, az adatok el fognak veszni!");
        while (true)
        {
            Console.Write("\n# Adjon meg egy parancsot: ");
            string? command = Console.ReadLine();
            if (command != null && command.Trim() != "")
            {
                EvaluateCommand(command);
            }
            else
            {
                Console.WriteLine("# Nem adtál meg parancsot!");
            }
        }
    }

    private void EvaluateCommand(string command)
    {
        if (command == Commands.AddCategory)
        {
            AddCategory();
        }
        else if (command == Commands.Exit)
        {
            SaveStateAndExitProgram();
        }
        else if (command == Commands.Help)
        {
            ListCommands();
        }
        else if (command == Commands.ListCategory)
        {
            ListCategory();
        }
        else if (command == Commands.AddTransaction)
        {
            AddTransaction();
        }
        else if (command == Commands.ListIncomes)
        {
            ListIncomes();
        }
        else if (command == Commands.ListExpenses)
        {
            ListExpenses();
        }
        else if (command == Commands.GetBalance)
        {
            GetBalance();
        }
        else if (command == Commands.GetSumIncome)
        {
            GetSumIncome();
        }
        else if (command == Commands.GetSumExpense)
        {
            GetSumExpense();
        }
        else if (command == Commands.ListExpensesByCategory)
        {
            ListExpensesByCategory();
        }
        else if (command == Commands.ListIncomesByCategory)
        {
            ListIncomesByCategory();
        }
        else
        {
            Console.WriteLine("Nem létezik ilyen parancs! Parancsok kilistázásához használd a 'help'-et!");
        }
    }

    private void AddCategory()
    {
        string? categoryName = null;
        string? isIncomeString = null;

        while (categoryName == null || categoryName.Trim() == "")
        {
            Console.Write("## Adjon meg egy nevet a kategóriának: ");
            categoryName = Console.ReadLine();
            if (categoryName == null || categoryName.Trim() == "")
            {
                Console.WriteLine("## Érvénytelen érték!");
            }
        }
        
        while (isIncomeString == null || isIncomeString.Trim() == "" || (isIncomeString != "+" && isIncomeString != "-"))
        {
            Console.Write("## Bevétel (+) vagy költség (-) kategória lesz: ");
            isIncomeString = Console.ReadLine();
            if (isIncomeString == null || isIncomeString.Trim() == "" || (isIncomeString != "+" && isIncomeString != "-"))
            {
                Console.WriteLine("## Érvénytelen érték! Adjon meg + vagy - karaktert!");
            }
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
        Type type = typeof(Commands);
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

        int i = 1;
        foreach (var name in categoryNames)
        {
            Console.WriteLine("\t " + i + ". " + name);
            i++;
        }
    }

    private void AddTransaction()
    {
        string? transactionName = null;
        string categoryId = "";
        int value = 0;

        while (transactionName == null || transactionName.Trim() == "")
        {
            Console.Write("## Adjon meg egy nevet a tranzakciónak: ");
            transactionName = Console.ReadLine();
            if (transactionName == null || transactionName.Trim() == "")
            {
                Console.WriteLine("## Érvénytelen érték!");
            }
        }
        
        Console.WriteLine("## Add meg a választott kategória sorszámát!");
        ListCategory();
        string? categoryString = null;
        bool isParsable = false;
        while (categoryString == null || categoryString.Trim() == "" || !isParsable)
        {
            Console.Write("## Választott kategória sorszáma: ");
            categoryString = Console.ReadLine();
            int categoryIndex;
            isParsable = Int32.TryParse(categoryString, out categoryIndex);
            if (categoryString == null || categoryString.Trim() == "" || !isParsable)
            {
                Console.WriteLine("## Érvénytelen érték!");
            }
            else
            {
                var categories = CategoryService.Instance.GetCategories()
                    .OrderBy(category => category.Name);
                categoryId = categories.ElementAt(categoryIndex - 1).Id;
            }
        }
        
        Console.WriteLine("## Add meg a tranzakció értékét (pozitív egész szám):");
        string? valueString = null;
        isParsable = false;
        while (valueString == null || valueString.Trim() == "" || !isParsable)
        {
            Console.Write("## Érték megadása: ");
            valueString = Console.ReadLine();
            isParsable = Int32.TryParse(valueString, out value);
            if (valueString == null || valueString.Trim() == "" || !isParsable)
            {
                Console.WriteLine("Érvénytelen érték!");
            }
        }
        
        TransactionService.Instance.AddTransaction(transactionName, value, DateTime.Today, categoryId);
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