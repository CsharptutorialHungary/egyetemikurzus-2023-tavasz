namespace GoogleBooks.Model;

public record GoogleApiSearchResult
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

    public record VolumeInfo
    {
        public string title { get; init; }
        public List<string> authors { get; init; }
        public string publisher { get; init; }
        public string publishedDate { get; init; }
        public string description { get; init; }
        public List<IndustryIdentifier> industryIdentifiers { get; init; }
        public ReadingModes readingModes { get; init; }
        public int pageCount { get; init; }
        public string printType { get; init; }
        public List<string> categories { get; init; }
        public string maturityRating { get; init; }
        public bool allowAnonLogging { get; init; }
        public string contentVersion { get; init; }
        public PanelizationSummary panelizationSummary { get; init; }
        public ImageLinks imageLinks { get; init; }
        public string language { get; init; }
        public string previewLink { get; init; }
        public string infoLink { get; init; }
        public string canonicalVolumeLink { get; init; }
        public Dimensions dimensions { get; init; }
        public string mainCategory { get; init; }
        public double averageRating { get; init; }
        public int ratingsCount { get; init; }

    }

    public record AccessInfo
    {
        public string country { get; init; }
        public string viewability { get; init; }
        public bool embeddable { get; init; }
        public bool publicDomain { get; init; }
        public string textToSpeechPermission { get; init; }
        public Epub epub { get; init; }
        public Pdf pdf { get; init; }
        public string webReaderLink { get; init; }
        public string accessViewStatus { get; init; }
        public bool quoteSharingAllowed { get; init; }
    }

    public record Dimensions
    {
        public string height { get; init; }
        public string width { get; init; }
        public string thickness { get; init; }
    }

    public record Epub
    {
        public bool isAvailable { get; init; }
        public string acsTokenLink { get; init; }
    }

    public record ImageLinks
    {
        public string smallThumbnail { get; init; }
        public string thumbnail { get; init; }
        public string small { get; init; }
        public string medium { get; init; }
        public string large { get; init; }
        public string extraLarge { get; init; }
    }

    public record IndustryIdentifier
    {
        public string type { get; init; }
        public string identifier { get; init; }
    }

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

    public record ListPrice
    {
        public int amount { get; init; }
        public string currencyCode { get; init; }
        public long amountInMicros { get; init; }
    }

    public record Offer
    {
        public int finskyOfferType { get; init; }
        public ListPrice listPrice { get; init; }
        public RetailPrice retailPrice { get; init; }
    }

    public record PanelizationSummary
    {
        public bool containsEpubBubbles { get; init; }
        public bool containsImageBubbles { get; init; }
    }

    public record Pdf
    {
        public bool isAvailable { get; init; }
        public string acsTokenLink { get; init; }
    }

    public record ReadingModes
    {
        public bool text { get; init; }
        public bool image { get; init; }
    }

    public record RetailPrice
    {
        public int amount { get; init; }
        public string currencyCode { get; init; }
        public long amountInMicros { get; init; }
    }


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

    public record SearchInfo
    {
        public string textSnippet { get; init; }
    }


}
