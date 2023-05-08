namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
    public record IndustryIdentifier
    {
        public string type { get; init; }
        public string identifier { get; init; }
    }


}
