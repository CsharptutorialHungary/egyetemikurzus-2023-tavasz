namespace DU0038.Model;

public class Transaction
{
    public string Id { get; init; }
    public string Name { get; }
    public int Value { get; }
    public DateTime Date { get; }
    public string CategoryId { get; }

    public Transaction(string name, int value, DateTime date, string categoryId)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Value = value;
        Date = date;
        CategoryId = categoryId;
    }
}