using DU0038.Builder;
using DU0038.Model;

namespace DU0038.Service;

public class TransactionService
{
    private static TransactionService? _instance = null;
    private static readonly object Padlock = new object();
    private List<Transaction> _transactions = new List<Transaction>();

    private TransactionService()
    {
    }

    public async Task InitializeTransactions()
    {
        _transactions = await FileService.Instance.ReadTransactionsFromFile();
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

    public void AddTransaction(string name, int value, DateTime date, string categoryId)
    {
        _transactions.Add(new TransactionBuilder()
            .WithName(name)
            .WithValue(value)
            .WithDate(date)
            .WithCategoryId(categoryId)
            .Build());
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

    public int GetSumIncome()
    {
        return (from incomeTransactions in GetIncomeTransactions() select incomeTransactions.Value).Sum();
    }
    
    public int GetSumExpense()
    {
        return (from expenseTransactions in GetExpenseTransactions() select expenseTransactions.Value).Sum();
    }

    public int GetBalance()
    {
        return GetSumIncome() - GetSumExpense();
    }
    
    public List<Transaction> GetExpensesByCategory(string categoryId)
    {
        return (from expenseTransactions in GetExpenseTransactions() 
            where expenseTransactions.CategoryId.Equals(categoryId) 
            select expenseTransactions).ToList();
    }
    
    public List<Transaction> GetIncomesByCategory(string categoryId)
    {
        return (from incomeTransactions in GetIncomeTransactions() 
            where incomeTransactions.CategoryId.Equals(categoryId) 
            select incomeTransactions).ToList();
    }
}