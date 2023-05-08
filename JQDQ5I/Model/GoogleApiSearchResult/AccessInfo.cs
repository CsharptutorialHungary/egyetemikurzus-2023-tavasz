namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
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
}