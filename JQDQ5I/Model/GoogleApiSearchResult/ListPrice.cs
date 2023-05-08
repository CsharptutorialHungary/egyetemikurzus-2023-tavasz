namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
    public record ListPrice
    {
        public int amount { get; init; }
        public string currencyCode { get; init; }
        public long amountInMicros { get; init; }
    }


}
