using DU0038.Service;

namespace DU0038.Controller;

public class ProgramController
{
    public ProgramController()
    {
    }

    public void StartProgramLoop()
    {
        while (true)
        {
            Console.WriteLine("Adjon meg egy parancsot: ");
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
        if (command == "add category")
        {
            HandleAddCategory();
        }
        else if (command == "exit")
        {
            SaveStateAndExitProgram();
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
}