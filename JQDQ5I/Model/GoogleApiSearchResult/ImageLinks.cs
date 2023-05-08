namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
    public record ImageLinks
    {
        public string smallThumbnail { get; init; }
        public string thumbnail { get; init; }
        public string small { get; init; }
        public string medium { get; init; }
        public string large { get; init; }
        public string extraLarge { get; init; }
    }
}
