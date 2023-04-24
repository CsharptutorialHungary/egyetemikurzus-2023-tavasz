using DU0038.Model;

namespace DU0038.Service;

public class TransactionService
{
    private static TransactionService? _instance = null;
    private static readonly object Padlock = new object();
    private List<Transaction> _transactions;

    public TransactionService()
    {
        _transactions = FileService.Instance.ReadTransactionsFromFile();
    }
    
    public static TransactionService Instance
    {
        get
        {
            lock (Padlock)
            {
                return _instance ??= new TransactionService();
            }
        }
    }

    public void AddTransaction(string name, uint value, DateTime date, string categoryId)
    {
        _transactions.Add(new Transaction(name, value, date, categoryId));
    }

    public void SaveTransactions()
    {
        FileService.Instance.WriteTransactionsToFile(_transactions);
    }
    
    public List<Transaction> GetTransactions()
    {
        return _transactions;
    }

    public List<Transaction> GetIncomeTransactions()
    {
        return (from transaction in _transactions
            join category in CategoryService.Instance.GetCategories() on
                transaction.CategoryId equals category.Id
            where category.IsIncome
            orderby transaction.Date
            select transaction).ToList();
    }
    
    public List<Transaction> GetExpenseTransactions()
    {
        return (from transaction in _transactions
            join category in CategoryService.Instance.GetCategories() on
                transaction.CategoryId equals category.Id
            where !category.IsIncome
            orderby transaction.Date
            select transaction).ToList();
    }
}