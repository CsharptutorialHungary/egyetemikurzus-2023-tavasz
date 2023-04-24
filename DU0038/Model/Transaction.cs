namespace DU0038.Model;

public class Transaction
{
    public string Id { get; init; }
    public string Name { get; set; }
    public int Value { get; set; }
    public DateTime Date { get; set; }
    public string CategoryId { get; set; }

    public Transaction(string name, int value, DateTime date, string categoryId)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Value = value;
        Date = date;
        CategoryId = categoryId;
    }
}