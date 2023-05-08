using JQDQ5I.Model.GoogleApiSearchResult;
using System.ComponentModel;

namespace GoogleBooks.Model;

public class SearchResult
{    
    public SearchResult(GoogleApiSearchResult.Item item) {
        Title = item.volumeInfo.title;
        Author = item.volumeInfo.authors?[0] ?? "-";
        Publisher = item.volumeInfo.publisher;
        PublicationYear = item.volumeInfo.publishedDate?.ToString();
        ISBN13 = item.volumeInfo.industryIdentifiers?.FirstOrDefault(i => i.type == "ISBN_13")?.identifier;
        PageCount = item.volumeInfo.pageCount.ToString();
    }

    [DisplayName("Cím")]
    public string Title { get; set; }

    [DisplayName("Szerző")]
    public string Author { get; set; }

    [DisplayName("Kiadó")]
    public string Publisher { get; set; }

    [DisplayName("Kiadási év")]
    public string PublicationYear { get; set; }

    [DisplayName("ISBN")]
    public string ISBN13 { get; set; }

    [DisplayName("Oldalszám")]
    public string PageCount { get; set; }
}
