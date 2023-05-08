namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
    public int totalItems { get; init; }
    public List<Item> items { get; init; }
    public string kind { get; init; }
    public string id { get; init; }
    public string etag { get; init; }
    public string selfLink { get; init; }
    public VolumeInfo volumeInfo { get; init; }
    public SaleInfo saleInfo { get; init; }
    public AccessInfo accessInfo { get; init; }

}
