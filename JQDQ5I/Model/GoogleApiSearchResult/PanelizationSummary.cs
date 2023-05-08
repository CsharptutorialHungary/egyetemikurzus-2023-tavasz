namespace JQDQ5I.Model.GoogleApiSearchResult;

public partial record GoogleApiSearchResult
{
    public record PanelizationSummary
    {
        public bool containsEpubBubbles { get; init; }
        public bool containsImageBubbles { get; init; }
    }


}
