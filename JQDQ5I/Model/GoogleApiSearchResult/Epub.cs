namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
    public record Epub
    {
        public bool isAvailable { get; init; }
        public string acsTokenLink { get; init; }
    }
}