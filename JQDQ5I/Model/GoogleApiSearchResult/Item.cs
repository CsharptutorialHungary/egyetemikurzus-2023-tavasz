namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
    public record Item
    {
        public string kind { get; init; }
        public string id { get; init; }
        public string etag { get; init; }
        public string selfLink { get; init; }
        public VolumeInfo volumeInfo { get; init; }
        public SaleInfo saleInfo { get; init; }
        public AccessInfo accessInfo { get; init; }
        public SearchInfo searchInfo { get; init; }
    }


}
