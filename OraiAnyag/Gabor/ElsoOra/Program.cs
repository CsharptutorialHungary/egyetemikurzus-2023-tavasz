internal class Program
{
    //private
    //protected
    //public
    //internal
    //protected internal
    //private protected
    //file

    private static void Main(string[] args)
    {
        //byte - 0- 255
        //sbyte - -127 - 127
        //short - 16bit
        //int - 32bit
        //long - 64bit
        //uint
        //ushort
        //ulong
        //char == ushort

        //reference vs value
        string alma = "alma 🍎" + "459"; //string immutable!
        string alma2 = "alma 🍎";
        Console.WriteLine(alma.Length);

        try
        {
            checked
            {
                Console.WriteLine("alma == alma: {0}", "alma" == "alma"); //false

                int overflowTest = int.MaxValue;
                overflowTest += int.Parse(Console.ReadLine());
                Console.WriteLine(overflowTest);
            }
        }
        catch (Exception ex) when (ex is FormatException || ex is OverflowException)
        {
            //pokémon exception handling
        }
    }
}