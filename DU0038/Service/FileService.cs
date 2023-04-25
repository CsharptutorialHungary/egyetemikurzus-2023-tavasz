using System.Text.Json;
using DU0038.Builder;
using DU0038.Model;

namespace DU0038.Service;

public class FileService
{
    private static FileService? _instance;
    private static readonly object Padlock = new();

    private FileService()
    {
    }

    public static FileService Instance
    {
        get
        {
            lock (Padlock)
            {
                return _instance ??= new FileService();
            }
        }
    }

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = true,
    };
    
    private readonly string _transactionsFilePath = Path.Combine(AppContext.BaseDirectory, "assets/transactions.txt");
    private readonly string _categoriesFilePath = Path.Combine(AppContext.BaseDirectory, "assets/categories.txt");
    
    private string SerializeTransactionList(List<Transaction> transactionList)
    {
        return JsonSerializer.Serialize(transactionList, _jsonSerializerOptions);
    }

    private List<Transaction>? DeserializeTransactionList(string serializedTransactionList)
    {
        return JsonSerializer.Deserialize<List<Transaction>>(serializedTransactionList, _jsonSerializerOptions);
    }
    
    private string SerializeCategoryList(List<Category> categoryList)
    {
        return JsonSerializer.Serialize(categoryList, _jsonSerializerOptions);
    }
    
    private List<Category>? DeserializeCategoryList(string serializedCategoryList)
    {
        return JsonSerializer.Deserialize<List<Category>>(serializedCategoryList, _jsonSerializerOptions);
    }

    public async void WriteTransactionsToFile(List<Transaction> transactions)
    {
        await using var file = File.OpenWrite(_transactionsFilePath);
        await using var writer = new StreamWriter(file);
        try
        {
            await writer.WriteLineAsync(SerializeTransactionList(transactions));
        }
        catch (IOException ex)
        {
            Console.WriteLine("Hiba történt a tranzakciók fájlba írása során!");
        }
    }
    
    public async void WriteCategoriesToFile(List<Category> categories)
    {
        await using var file = File.OpenWrite(_categoriesFilePath);
        await using var writer = new StreamWriter(file);
        try
        {
            await writer.WriteLineAsync(SerializeCategoryList(categories));
        }
        catch (IOException ex)
        {
            Console.WriteLine("Hiba történt a kategóriák fájlba írása során!");
        }
    }

    public List<Transaction> ReadTransactionsFromFile()
    {
        var transactions = new List<Transaction>();
        try
        {
            var serializedTransactions = File.ReadAllText(_transactionsFilePath);
            transactions = DeserializeTransactionList(serializedTransactions);
        }
        catch (FileNotFoundException)
        {
            WriteTransactionsToFile(new List<Transaction>());
            ReadTransactionsFromFile();
        }
        catch (IOException)
        {
            Console.WriteLine("Hiba történt a tranzakció fájl olvasása során!");
        }
        return transactions ?? new List<Transaction>();
    }
    
    public List<Category> ReadCategoriesFromFile()
    {
        var categories = new List<Category>();
        try
        {
            var serializedCategories = File.ReadAllText(_categoriesFilePath);
            categories = DeserializeCategoryList(serializedCategories);
        }
        catch (FileNotFoundException)
        {
            WriteCategoriesToFile(new List<Category>());
            ReadCategoriesFromFile();
        }
        catch (IOException)
        {
            Console.WriteLine("Hiba történt a kategóriák fájl olvasása során!");
        }
        return categories ?? new List<Category>();
    }
}