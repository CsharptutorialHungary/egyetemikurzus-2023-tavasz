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
        Console.WriteLine("##### Üdvözöllek az alkalmazásban #####");
        Console.WriteLine("A megadható parancsok listázásához add meg a 'help' parancsot!");
        Console.WriteLine("Ha az alkalmazásból nem az 'exit' parancsal lépsz ki, az adatok el fognak veszni!");
        while (true)
        {
            Console.WriteLine("\n# Adjon meg egy parancsot: ");
            string? command = Console.ReadLine();
            if (command != null && command.Trim() != "")
            {
                EvaluateCommand(command);
            }
            else
            {
                Console.WriteLine("Nem adtál meg parancsot!");
            }
        }
    }

    private void EvaluateCommand(string command)
    {
        if (command == Commands.AddCategory)
        {
            HandleAddCategory();
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
    }

    private void HandleAddCategory()
    {
        string? categoryName = null;
        string? isIncomeString = null;

        while (categoryName == null || categoryName.Trim() == "")
        {
            Console.WriteLine("Adjon meg egy nevet a kategóriának:");
            categoryName = Console.ReadLine();
            if (categoryName == null || categoryName.Trim() == "")
            {
                Console.WriteLine("Érvénytelen érték!");
            }
        }
        
        while (isIncomeString == null || isIncomeString.Trim() == "" || (isIncomeString != "+" && isIncomeString != "-"))
        {
            Console.WriteLine("Bevétel (+) vagy költség (-) kategória lesz:");
            isIncomeString = Console.ReadLine();
            if (isIncomeString == null || isIncomeString.Trim() == "" || (isIncomeString != "+" && isIncomeString != "-"))
            {
                Console.WriteLine("Érvénytelen érték! Adjon meg + vagy - karaktert!");
            }
        }
        
        CategoryService.Instance.AddCategory(categoryName, isIncomeString == "+");
        Console.WriteLine("Kategória hozzáadása sikeres!");
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
        
        foreach (var name in categoryNames)
        {
            Console.WriteLine("\t - " + name);
        }
    }
}