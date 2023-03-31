namespace reflection_linq
{
    internal class HelloWorldCommand : ICommand
    {
        public string Name => "hello";

        public void Execute()
        {
            Console.WriteLine("Hello World");
        }
    }
}
