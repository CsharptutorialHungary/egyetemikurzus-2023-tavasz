namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
    public record SaleInfo
    {
        public string country { get; init; }
        public string saleability { get; init; }
        public bool isEbook { get; init; }
        public ListPrice listPrice { get; init; }
        public RetailPrice retailPrice { get; init; }
        public string buyLink { get; init; }
        public List<Offer> offers { get; init; }
    }


}
