namespace JQDQ5I.Model.GoogleApiSearchResult;
public partial record GoogleApiSearchResult
{
    public record Dimensions
    {
        public string height { get; init; }
        public string width { get; init; }
        public string thickness { get; init; }
    }

}
