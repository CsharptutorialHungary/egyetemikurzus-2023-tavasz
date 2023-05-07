internal class Program
{
    private static void Main(string[] args)
    {
        string fileName = "2022.csv";
        string path = Path.Combine(Environment.CurrentDirectory, fileName);

        using (var reader = new StreamReader(path))
        {
            //do smth
        }
    }
}