namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
    public record Offer
    {
        public int finskyOfferType { get; init; }
        public ListPrice listPrice { get; init; }
        public RetailPrice retailPrice { get; init; }
    }


}
