namespace GoogleBooks.Exception;


[Serializable]
public class SearchException : System.Exception
{
    public SearchException() : base() { }
    public SearchException(string message, bool isError = false) : base(isError ? "Search failed:\n" + message : message){}
    public SearchException(string message, System.Exception innerException) : base(message, innerException) { }
    protected SearchException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
