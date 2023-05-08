namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
    public record ReadingModes
    {
        public bool text { get; init; }
        public bool image { get; init; }
    }


}
