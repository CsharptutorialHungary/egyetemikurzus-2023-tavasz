namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
    public record Pdf
    {
        public bool isAvailable { get; init; }
        public string acsTokenLink { get; init; }
    }


}
