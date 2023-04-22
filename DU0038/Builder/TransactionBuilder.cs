using DU0038.Model;

namespace DU0038.Builder;

public class TransactionBuilder
{
    private string _id;
    private string _name;
    private uint _value;
    private DateTime _date;
    private string _categoryId;
    
    public TransactionBuilder()
    {
        _id = "";
        _name = "";
        _value = 0;
        _date = new DateTime(0);
        _categoryId = "";
    }
    
    public TransactionBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public TransactionBuilder WithValue(uint value)
    {
        _value = value;
        return this;
    }
    
    public TransactionBuilder WithDate(DateTime date)
    {
        _date = date;
        return this;
    }
    
    public TransactionBuilder WithCategoryId(string categoryId)
    {
        _categoryId = categoryId;
        return this;
    }

    public Transaction Build()
    {
        if (string.IsNullOrEmpty(_name))
        {
            throw new InvalidOperationException("Name is required.");
        }

        if (_value <= 0)
        {
            throw new InvalidOperationException("Price must be greater than zero.");
        }

        if (_date.CompareTo(new DateTime(0)) == 0)
        {
            throw new InvalidOperationException("Date is required");
        }

        if (string.IsNullOrEmpty(_categoryId))
        {
            throw new InvalidOperationException("Category is required.");
        }
        
        return new Transaction(_name, _value, _date, _categoryId);
    }
}