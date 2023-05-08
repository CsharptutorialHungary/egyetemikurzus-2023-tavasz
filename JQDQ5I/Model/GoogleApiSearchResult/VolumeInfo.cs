namespace JQDQ5I.Model.GoogleApiSearchResult;
public partial record GoogleApiSearchResult
{
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
}
